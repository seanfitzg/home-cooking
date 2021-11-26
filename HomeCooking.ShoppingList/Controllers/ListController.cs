using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.ShoppingList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
            nameof(DefaultApiConventions.Get))]       
        public string CreateList()
        {
            return "OK";
        }
    }
}