using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public class SqlLiteRecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _recipeContext;

        public SqlLiteRecipeRepository(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }
        
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeContext.Recipes.ToList();
        }

        public void AddRecipe(Recipe recipe)
        {
            _recipeContext.Recipes.Add(recipe);
            _recipeContext.SaveChanges();
        }
    }
}