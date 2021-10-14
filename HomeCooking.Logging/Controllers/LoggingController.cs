using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapr;
using HomeCooking.Domain.Entities;
using HomeCooking.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeCooking.Logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : Controller
    {
        private readonly ILogger<LoggingController> _logger;
        private static IList<string> _createdRecipes = new List<string>();
        
        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }
        
        [Topic("pubsub-homecooking", "newrecipe")]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
            nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public void Index(RecipeCreated recipeCreated)
        {
            _createdRecipes.Add(recipeCreated.Name);
            _logger.Log(LogLevel.Information, $"Recipe created: {recipeCreated.Name}");
        }
        
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
            nameof(DefaultApiConventions.Get))]        
        public string Index()
        {
            var recipes = string.Join(",", _createdRecipes);
            _logger.Log(LogLevel.Information, $"Get recipes - {recipes}");
            return $@"Recipes recently created: {recipes}";
        }
    }
}