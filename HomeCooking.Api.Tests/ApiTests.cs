using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using HomeCooking.Application.DTOs;
using Xunit;

namespace HomeCooking.Api.Tests
{
    public class ApiTests : IClassFixture<SelfHostedApi>
    {
        private readonly SelfHostedApi _api;
        private static readonly Fixture Fixture = new();
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        
        public ApiTests(SelfHostedApi api)
        {
            _api = api;
        }
        
        [Fact]
        public async void IndexShouldReturnAListOfRecipes()
        {
            var client = _api.CreateClient();
            
            var response = await client.GetAsync("/Recipes");
            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
            
            var returnData = await response.Content.ReadAsStringAsync();
            var recipes = JsonSerializer.Deserialize<IList<RecipeDto>>(returnData);
        }
        
        [Fact]
        public async void AddingARecipeShouldSaveToDatabase()
        {
            var client = _api.CreateClient();
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
            var recipeDto = await GetRecipeById(1);
            var newIngredient = Fixture.Create<IngredientDto>();
            var client = _api.CreateClient();

            recipeDto.Name = "Update Name";
            recipeDto.Method = "Update Method";
            recipeDto.Description = "Update Description";
            recipeDto.UserId = "Test User";
            
            var ingredients = recipeDto.Ingredients.Append(newIngredient).ToList();
            recipeDto.Ingredients = ingredients;
            var payload = JsonSerializer.Serialize(recipeDto);
            
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Recipes", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var updatedRecipe = await GetRecipeById(1);
            Assert.Equal(recipeDto.Name, updatedRecipe.Name);
            Assert.Equal(recipeDto.Method, updatedRecipe.Method);
            Assert.Equal(recipeDto.Description, updatedRecipe.Description);
            Assert.Equal(SelfHostedApi.TestUser, updatedRecipe.UserId);
            Assert.Equal(newIngredient, recipeDto.Ingredients.Last());
        }
        
        [Fact]
        public async void DeletingARecipeShouldDeleteARecipe()
        {
            var client = _api.CreateClient();

            var response = await client.DeleteAsync("/Recipes/1");
            response.EnsureSuccessStatusCode();

            var recipeDto = await GetRecipeById(1);
            Assert.Null(recipeDto);

        }
        
        private async Task<RecipeDto> GetRecipeById(int id)
        {
            var client = _api.CreateClient();
            var response = await client.GetAsync($"/Recipes/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            var returnData = await response.Content.ReadAsStringAsync();
            if (returnData == "") return null;
            var recipeDto = JsonSerializer.Deserialize<RecipeDto>(returnData, _options);
            return recipeDto;
        }
    }
}