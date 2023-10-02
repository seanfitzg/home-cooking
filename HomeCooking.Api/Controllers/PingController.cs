using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "ok from ping";
        }
    }
}