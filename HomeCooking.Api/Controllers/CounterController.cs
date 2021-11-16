using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;
using HomeCooking.Domain.Actors;
using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly IActorProxyFactory _actorProxyFactory;

        public CounterController(IActorProxyFactory actorProxyFactory)
        {
            _actorProxyFactory = actorProxyFactory;
        }

        [HttpPut("{counterId}")]
        public Task<int> IncrementAsync(string counterId)
        {
            var scoreActor = _actorProxyFactory.CreateActorProxy<ICounterActor>(
                new ActorId(counterId),
                "CounterActor");

            return scoreActor.IncrementCounterAsync();
        }
    }
}