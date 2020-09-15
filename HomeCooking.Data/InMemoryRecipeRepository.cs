using System.Collections.Generic;
using System.Threading.Tasks;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Data
{
    public class InMemoryRecipeRepository : IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes(string userId)
        {
            return new [] {
                new Recipe()
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

        public void AddRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public Recipe GetById(int recipeId)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}