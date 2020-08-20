using System.Collections.Generic;
using HomeCooking.data.Models;

namespace HomeCooking.api.Services
{
    class RecipeRepository : IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return new [] {
                new Recipe
                {
                    Id = 1,
                    Method = "Toast the bread, nuke the beans, butter the bread, pour beans over bread, sprinkle with cheese",
                    Name = "Cheesy Beans on Toast",
                    Ingredients = new []
                    {
                        new Ingredient()
                        {
                            FoodType = "Beans",
                            Other = new Other(1, "Can")
                        },
                        new Ingredient()
                        {
                            FoodType = "Bread",
                            Other = new Other(1, "Can")
                        },
                        new Ingredient()
                        {
                            FoodType = "Grated Cheese"
                        },                    
                        new Ingredient()
                        {
                            FoodType = "Butter"
                        },                                                
                    }
                },
            };
        }
    }
}