using System.Collections.Generic;
using System.Linq;
using HomeCooking.api.Models.DTOs;
using HomeCooking.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        [HttpGet]
        public IEnumerable<RecipeDto> Index()
        {
            var recipes = _recipeRepository.GetAllRecipes();
            return recipes.Select(r => new RecipeDto(r.Id, r.Name));
        }
    }
}