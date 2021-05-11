using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public class SqlRecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _recipeContext;

        public SqlRecipeRepository(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }
        
        public IEnumerable<Recipe> GetAllRecipes(string userId)
        {
            return _recipeContext.Recipes.Where(r => r.UserId == userId).ToList();
        }

        public int AddRecipe(Recipe recipe)
        {
            _recipeContext.Recipes.Add(recipe);
            return _recipeContext.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _recipeContext.Update(recipe);
            _recipeContext.SaveChanges();
        }

        public Recipe GetById(int recipeId)
        {
            var recipe = _recipeContext.Recipes.SingleOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                throw new ApplicationException("Recipe not found"); 
            }
            return recipe;
        }

        public void Delete(Recipe recipe)
        {
            _recipeContext.Recipes.Remove(recipe);
            _recipeContext.SaveChanges();
        }
    }
}