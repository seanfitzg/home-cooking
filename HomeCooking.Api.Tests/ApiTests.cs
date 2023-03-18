using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using HomeCooking.Application;
using HomeCooking.Application.DTOs;
using Xunit;

namespace HomeCooking.Api.Tests
{
    public class ApiTests : IClassFixture<SelfHostedApi>
    {
        private readonly HttpClient _client;
        private readonly int _recipeId;
        private static readonly Fixture Fixture = new();
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
        };
        
        public ApiTests(SelfHostedApi api)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "ApiTests");
            _client = api.CreateClient();
            _recipeId = CreateRecipe(_client);
        }

        private static CreateRecipeCommand BuildCreateRecipeCommand()
        {
            return Fixture.Build<CreateRecipeCommand>().Create();
        }
        
        private static int CreateRecipe(HttpClient client)
        {
            var createRecipeCommand = BuildCreateRecipeCommand();
            var payload = JsonSerializer.Serialize(createRecipeCommand);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = client.PostAsync("/Recipes", content).Result;
            response.EnsureSuccessStatusCode();
            var id = response.Content.ReadAsStringAsync().Result;
            return int.Parse(id);
        }

        [Fact]
        public async void TestPing()
        {
            var response = await _client.GetAsync("/Ping");
            response.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async void IndexShouldReturnAListOfRecipes()
        {
            var response = await _client.GetAsync("/Recipes");
            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType?.ToString());
            
            var returnData = await response.Content.ReadAsStringAsync();
            var recipes = JsonSerializer.Deserialize<IList<RecipeDto>>(returnData);
            Assert.NotNull(recipes);
        }
        
        [Fact]
        public async void AddingARecipeShouldSaveToDatabase()
        {
            var createRecipeCommand = BuildCreateRecipeCommand();
            var payload = JsonSerializer.Serialize(createRecipeCommand);
            
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Recipes", content);
            response.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var recipeId = await response.Content.ReadAsStringAsync();

            var addedRecipe = await GetRecipeById(_client, int.Parse(recipeId));
            Assert.Equal(createRecipeCommand.Name, addedRecipe.Name);
            
        }
                
        [Fact]
        public async void GettingARecipeShouldReturnARecipe()
        {
            var recipe = await GetRecipeById(_client, _recipeId);
            Assert.Equal(_recipeId, recipe.Id);
        }

        [Fact]
        public async void UpdatingARecipeShouldUpdateARecipe()
        {
            var recipeDto = await GetRecipeById(_client, _recipeId);
            var newIngredient = Fixture.Create<IngredientDto>();

            recipeDto.Name = "Update Name";
            recipeDto.Method = "Update Method";
            recipeDto.Description = "Update Description";
            recipeDto.UserId = "Test User";
            
            var ingredients = recipeDto.Ingredients.Append(newIngredient).ToList();
            recipeDto.Ingredients = ingredients;
            var payload = JsonSerializer.Serialize(recipeDto);
            
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/Recipes", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var updatedRecipe = await GetRecipeById(_client, _recipeId);
            Assert.Equal(recipeDto.Name, updatedRecipe.Name);
            Assert.Equal(recipeDto.Method, updatedRecipe.Method);
            Assert.Equal(recipeDto.Description, updatedRecipe.Description);
            Assert.Equal(SelfHostedApi.TestUser, updatedRecipe.UserId);
            Assert.Equal(newIngredient, recipeDto.Ingredients.Last());
        }
        
        [Fact]
        public async void DeletingARecipeShouldDeleteARecipe()
        {
            var id = CreateRecipe(_client);
            
            var response = await _client.DeleteAsync($"/Recipes/{id}");
            response.EnsureSuccessStatusCode();

            var recipeDto = await GetRecipeById(_client, id);
            Assert.Null(recipeDto);

        }
        
        private async Task<RecipeDto> GetRecipeById(HttpClient client, int id)
        {
            var response = await client.GetAsync($"/Recipes/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            var returnData = await response.Content.ReadAsStringAsync();
            if (returnData == "") return null;
            var recipeDto = JsonSerializer.Deserialize<RecipeDto>(returnData, _options);
            return recipeDto;
        }
    }


}