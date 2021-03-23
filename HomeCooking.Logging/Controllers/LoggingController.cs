using System;
using System.Net;
using System.Threading.Tasks;
using Dapr;
using HomeCooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.Logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : Controller
    {
        [Topic("pubsub", "newRecipe")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
            nameof(DefaultApiConventions.Post))]        
        public async Task<IActionResult> Index(Recipe recipe)
        {
            Console.WriteLine($"Recipe created: {recipe.Name}");
            return await Task.FromResult(NoContent());
        }
    }
}