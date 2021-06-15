using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            var existingRecipe = GetById(recipe.Id);
            _recipeContext.Entry(existingRecipe).CurrentValues.SetValues(recipe);
            foreach (var ingredient in existingRecipe.Ingredients)
            {
                if (recipe.Ingredients.All(p => p.Id != ingredient.Id))
                {
                    _recipeContext.Ingredients.Remove(ingredient);
                }    
            }
            
            foreach (var ingredient in recipe.Ingredients)
            {
                var existingIngredient = existingRecipe.Ingredients.SingleOrDefault(p => p.Id == ingredient.Id);
                if (existingIngredient == null)
                {
                    existingRecipe.Ingredients.Add(ingredient);
                }
                else
                {
                    _recipeContext.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
                }
            }
            _recipeContext.SaveChanges();
        }

        public Recipe GetById(int recipeId)
        {
            var recipe = _recipeContext.Recipes
                        .Include(p => p.Ingredients)
                        .SingleOrDefault(r => r.Id == recipeId);
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