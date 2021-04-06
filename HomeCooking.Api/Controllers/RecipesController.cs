using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Api.DTOs;
using HomeCooking.Api.EventBus;
using HomeCooking.Application;
using HomeCooking.Domain.Entities;
using HomeCooking.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeCooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IMediator mediator, IEventBus eventBus, ILogger<RecipesController> logger)
        {
            _mediator = mediator;
            _eventBus = eventBus;
            _logger = logger;
        }
        
        [HttpGet]
        [Authorize("read:recipes")]
        public async Task<IEnumerable<RecipeListDto>> Index()
        {
            try
            {
                var recipes = await _mediator.Send(new GetAllRecipesCommand(HttpContext.User.Identity.Name));
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
        public async Task<Recipe> GetById(int recipeId)
        {
            try
            {
                var recipe = await _mediator.Send(new GetRecipeCommand(recipeId));
                return recipe;
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                throw;
            }
        }  
        
        [HttpPost]
        [Authorize("read:recipes")]
        public async Task<IActionResult> PostRecipe([FromBody] CreateRecipeCommand createRecipeCommand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                createRecipeCommand.UserId = HttpContext.User.Identity.Name;
                var id = await _mediator.Send(createRecipeCommand);
                await _eventBus.Send("newrecipe", new RecipeCreated(id, createRecipeCommand.Name));
                _logger.Log(LogLevel.Information, $"Recipe created: {id}.");
                return CreatedAtAction("Index", new { id }, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        [Authorize("read:recipes")]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeCommand updateRecipeCommand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                updateRecipeCommand.UserId = HttpContext.User.Identity.Name;
                await _mediator.Send(updateRecipeCommand);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}