using System.Collections.Generic;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public class SqlLiteRecipeRepository : IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes()
        {
            throw new System.NotImplementedException();
        }
    }
}