using System.Collections.Generic;
using HomeCooking.data.Models;

namespace HomeCooking.api.Services
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes();
    }
}