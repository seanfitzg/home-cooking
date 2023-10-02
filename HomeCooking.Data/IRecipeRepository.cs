using System.Collections.Generic;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes(string userId);
        int AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        Recipe GetById(int recipeId);
        void Delete(Recipe recipe);
    }
}