using System.Collections.Generic;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes();
    }
}