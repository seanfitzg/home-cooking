using System.Collections.Generic;
using System.Threading.Tasks;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes();
        Task AddRecipe(Recipe recipe);
    }
}