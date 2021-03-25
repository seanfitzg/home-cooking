using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using HomeCooking.Api.DTOs;
using HomeCooking.Application;
using HomeCooking.Data;
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
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMediator _mediator;
        private readonly DaprClient _daprClient;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeRepository recipeRepository, IMediator mediator, DaprClient daprClient, ILogger<RecipesController> logger)
        {
            _recipeRepository = recipeRepository;
            _mediator = mediator;
            _daprClient = daprClient;
            _logger = logger;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            createRecipeCommand.UserId = HttpContext.User.Identity.Name;
            var id = await _mediator.Send(createRecipeCommand);
            await _daprClient.PublishEventAsync("pubsub-homecooking", "newrecipe", new RecipeCreated(id, createRecipeCommand.Name));
            _logger.Log(LogLevel.Information, $"Recipe created: {id}.");
            return CreatedAtAction("Index", new { id }, id);
        }
        
        [HttpPut]
        [Authorize("read:recipes")]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeCommand updateRecipeCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            updateRecipeCommand.UserId = HttpContext.User.Identity.Name;
            await _mediator.Send(updateRecipeCommand);
            return Ok();
        }
        
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}