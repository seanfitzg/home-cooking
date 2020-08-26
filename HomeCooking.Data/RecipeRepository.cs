using System.Collections.Generic;
using System.Threading.Tasks;
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
                            Other = new Other()
                            {
                                Amount = 1,
                                AmountType = "Can"
                            }
                        },
                        new Ingredient()
                        {
                            FoodType = "Sliced Pan",
                            Other = new Other()
                            {
                                Amount = 1,
                                AmountType = "Packet"
                            }
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

        public Task AddRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}