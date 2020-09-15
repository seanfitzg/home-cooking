using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Api.DTOs;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

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
            try
            {
                var recipes = _recipeRepository.GetAllRecipes(HttpContext.User.Identity.Name);
                return recipes.Select(r => new RecipeListDto(r.Id, r.Name, r.Description));
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
                throw;
            }
        }       
        
        [HttpGet]
        [Route("{recipeId}")]
        [Authorize("read:recipes")]
        public Recipe GetById(int recipeId)
        {
            try
            {
                return _recipeRepository.GetById(recipeId);
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
                throw;
            }
        }      
        
        [HttpDelete]
        [Authorize("read:recipes")]
        public OkResult Delete([FromBody] Recipe recipe)
        {
            try
            {
                _recipeRepository.Delete(recipe);
                return Ok();
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
                throw;
            }
        }  
        
        [HttpPost]
        [Authorize("read:recipes")]
        public IActionResult PostRestaurant([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            recipe.UserId = HttpContext.User.Identity.Name;
            _recipeRepository.AddRecipe(recipe);

            return CreatedAtAction("Index", new { id = recipe.Id }, recipe);
        }
        
        [HttpPut]
        [Authorize("read:recipes")]
        public IActionResult UpdateRestaurant([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            recipe.UserId = HttpContext.User.Identity.Name;
            _recipeRepository.UpdateRecipe(recipe);
            return this.Ok();
        }
        
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}