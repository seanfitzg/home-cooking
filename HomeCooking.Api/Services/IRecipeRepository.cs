using System.Collections.Generic;
using HomeCooking.Api.Domain;

namespace HomeCooking.Api.Services
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes();
    }
}