using System.Collections.Generic;
using System.Threading.Tasks;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes(string userId);
        void AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        Recipe GetById(int recipeId);
        void Delete(Recipe recipe);
    }
}