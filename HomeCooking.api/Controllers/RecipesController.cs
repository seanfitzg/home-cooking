using System.Collections.Generic;
using HomeCooking.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Recipes : Controller
    {
        [HttpGet]
        public IEnumerable<Recipe> AllRecipes()
        {
            return new []
            {
                new Recipe
                {
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