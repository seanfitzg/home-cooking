using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Api.DTOs;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("read:recipes")]
        public IEnumerable<RecipeListDto> Index()
        {
            var recipes = _recipeRepository.GetAllRecipes();
            return recipes.Select(r => new RecipeListDto(r.Id, r.Name));
        }       
        
        [HttpPost]
        [Authorize("read:recipes")]
        public IActionResult PostRestaurant([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recipeRepository.AddRecipe(recipe);

            return CreatedAtAction("Index", new { id = recipe.Id }, recipe);
        }
    }
}