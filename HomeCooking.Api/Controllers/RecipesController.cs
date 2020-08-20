using System.Collections.Generic;
using System.Linq;
using HomeCooking.Api.DTOs;
using HomeCooking.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.Api.Controllers
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