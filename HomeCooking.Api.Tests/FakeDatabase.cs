using System.Collections.Generic;
using AutoFixture;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Api.Tests
{
    internal class FakeDatabase : IRecipeRepository
    {
        private static readonly Fixture Fixture = new Fixture();
        private readonly IList<Recipe> _recipes;
        
        public FakeDatabase()
        {
            _recipes = new List<Recipe>()
            {
                CreateRecipe()
            };
        }

        public static Recipe CreateRecipe()
        {
            return Fixture.Build<Recipe>().Create();
        }
        
        public IEnumerable<Recipe> GetAllRecipes(string userId)
        {
            return _recipes;
        }

        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public Recipe GetById(int recipeId)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}