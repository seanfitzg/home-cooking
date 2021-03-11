using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using HomeCooking.Domain.Entities;
using Xunit;

namespace HomeCooking.Api.Tests
{
    public class ApiTests : IClassFixture<SelfHostedApi>
    {
        private readonly SelfHostedApi _factory;

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
            var recipe = FakeDatabase.CreateRecipe();
            var payload = JsonSerializer.Serialize(recipe);
            
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Recipes", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}