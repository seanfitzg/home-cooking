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
                            FoodItem = "Baked Beans",
                            Quantity = 1,
                            QuantityType = "Tins",
                        },
                        new Ingredient()
                        {
                            FoodItem = "Sliced Pan",
                            Quantity = 1,
                            QuantityType = "Packets",
                        },
                        new Ingredient()
                        {
                            FoodItem = "Grated Cheese",
                            Quantity = 1,
                            QuantityType = "Packets",                            
                        },                    
                        new Ingredient()
                        {
                            FoodItem = "Butter",
                            Quantity = 1,                            
                        },                                                
                    }
                },
            };
        }

        public int AddRecipe(Recipe recipe)
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