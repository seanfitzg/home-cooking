using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Application;
using HomeCooking.Domain.Entities;
using Xunit;

namespace HomeCooking.Api.Tests
{
    public class ApiTests : IClassFixture<SelfHostedApi>
    {
        private readonly SelfHostedApi _factory;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        
        public ApiTests(SelfHostedApi factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async void IndexShouldReturnAListOfRecipes()
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("/Recipes");
            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
            
            var returnData = await response.Content.ReadAsStringAsync();
            var recipes = JsonSerializer.Deserialize<IList<Recipe>>(returnData);
        }
        
        [Fact]
        public async void AddingARecipeShouldSaveToDatabase()
        {
            var client = _factory.CreateClient();
            var createRecipeCommand = FakeDatabase.BuildCreateRecipeCommand();
            var payload = JsonSerializer.Serialize(createRecipeCommand);
            
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Recipes", content);
            response.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var recipeId = await response.Content.ReadAsStringAsync();

            var addedRecipe = await GetRecipeById(int.Parse(recipeId));
            Assert.Equal(createRecipeCommand.Name, addedRecipe.Name);
            
        }
                
        [Fact]
        public async void GettingARecipeShouldReturnARecipe()
        {
            var recipe = await GetRecipeById(1);
            Assert.Equal(1, recipe.Id);
        }

        [Fact]
        public async void UpdatingARecipeShouldUpdateARecipe()
        {
            var updateRecipe = await GetRecipeById(1);
            
            var client = _factory.CreateClient();
            var updateRecipeCommand = new UpdateRecipeCommand(1, "Sean", "Update Name", "Update Method", "Update Description", updateRecipe.Ingredients);
            
            var payload = JsonSerializer.Serialize(updateRecipeCommand);
            
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Recipes", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var updatedRecipe = await GetRecipeById(1);
            Assert.Equal(updateRecipeCommand.Name, updatedRecipe.Name);
            
        }
        
        [Fact]
        public async void DeletingARecipeShouldDeleteARecipe()
        {
            var client = _factory.CreateClient();
            var deleteRecipeCommand = new DeleteRecipeCommand(1);
            
            var payload = JsonSerializer.Serialize(deleteRecipeCommand);

            var response = await client.DeleteAsync("/Recipes/1");
            response.EnsureSuccessStatusCode();

            var updatedRecipe = await GetRecipeById(1);
            Assert.Null(updatedRecipe);

        }
        
        private async Task<Recipe> GetRecipeById(int id)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/Recipes/{id}");
            
            var returnData = await response.Content.ReadAsStringAsync();
            if (returnData == "") return null;
            var recipe = JsonSerializer.Deserialize<Recipe>(returnData, _options);
            return recipe;
        }
    }
}