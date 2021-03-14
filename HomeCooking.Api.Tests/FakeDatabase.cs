using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using HomeCooking.Application;
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
                CreateRecipe(1)
            };
        }

        public static CreateRecipeCommand BuildCreateRecipeCommand()
        {
            return Fixture.Build<CreateRecipeCommand>().Create();
        }
        
        public static Recipe CreateRecipe(int id)
        {
            return Fixture.Build<Recipe>().With(p => p.Id, id).Create();
        }
        
        public IEnumerable<Recipe> GetAllRecipes(string userId)
        {
            return _recipes;
        }

        public int AddRecipe(Recipe recipe)
        {
            recipe.Id = Fixture.Create<int>();
            _recipes.Add(recipe);
            return recipe.Id;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var originalRecipe = _recipes.Single(p => p.Id == recipe.Id);
            var index = _recipes.IndexOf(originalRecipe);
            _recipes[index] = recipe;
        }

        public Recipe GetById(int recipeId)
        {
            return _recipes.SingleOrDefault(r => r.Id == recipeId);
        }

        public void Delete(Recipe recipe)
        {
            _recipes.Remove(recipe);
        }
    }
}