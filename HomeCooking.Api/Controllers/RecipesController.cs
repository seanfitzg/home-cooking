using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeCooking.Application;
using HomeCooking.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeCooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RecipesController> _logger;
        private readonly string _userId;

        public RecipesController(IMediator mediator, ILogger<RecipesController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _logger = logger;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        
        [HttpGet]
        [Authorize("read:recipes")]
        public async Task<IEnumerable<RecipeListDto>> Index()
        {
            try
            {
                var recipes = await _mediator.Send(new GetAllRecipesCommand(_userId));
                return recipes.Select(r => new RecipeListDto(r.Id, r.Name, r.Description));
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpGet]
        [Route("{recipeId}")]
        [Authorize("read:recipes")]
        public async Task<ActionResult<RecipeDto>> GetById(int recipeId)
        {
            try
            {
                var recipe = await _mediator.Send(new GetRecipeCommand(recipeId));
                if (recipe == null) return NotFound();
                return new RecipeDto(recipe);
            }
            catch (Exception e)
            { 
                LogError(e);
                throw;
            }
        }      
        
        [HttpDelete]
        [Route("{recipeId}")]        
        [Authorize("read:recipes")]
        public async Task<OkResult> Delete(int recipeId)
        {
            try
            {
                await _mediator.Send(new DeleteRecipeCommand(recipeId));
                return Ok();
            }
            catch (Exception e)
            { 
                LogError(e);
                throw;
            }
        }  
        
        [HttpPost]
        [Authorize("read:recipes")]
        public async Task<IActionResult> PostRecipe([FromBody] RecipeDto recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var id = await _mediator.Send(new CreateRecipeCommand(recipe, _userId));
                return CreatedAtAction("Index", new { id }, id);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpPut]
        [Authorize("read:recipes")]
        public async Task<IActionResult> UpdateRecipe([FromBody] RecipeDto recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _mediator.Send(new UpdateRecipeCommand(recipe, _userId));
                return Ok();
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [Route("/error")]
        [HttpGet]
        public IActionResult Error() => Problem();
        
        private void LogError(Exception e)
        {
            _logger.Log(LogLevel.Error, e.ToString());
            Console.WriteLine(e);
        }

    }
}