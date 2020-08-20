using System.Collections.Generic;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public class RecipeRepository : IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return new [] {
                new Recipe
                {
                    Id = 1,
                    Method = "Toast the bread, microwave the beans, butter the bread, pour beans over bread, sprinkle with cheese",
                    Name = "Cheesy Beans on Toast.",
                    Ingredients = new []
                    {
                        new Ingredient()
                        {
                            FoodType = "Baked Beans",
                            Other = new Other(1, "Can")
                        },
                        new Ingredient()
                        {
                            FoodType = "Sliced Pan",
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