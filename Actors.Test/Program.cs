using System;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;
using HomeCooking.Domain.Actors;

namespace Actors.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var actorId1 = new ActorId("scoreActor1");
            var actorId2 = new ActorId("scoreActor2");

            var proxy1 = ActorProxy.Create<ICounterActor>(actorId1, "CounterActor");
            var proxy2 = ActorProxy.Create<ICounterActor>(actorId2, "CounterActor");

            var score1 = await proxy1.IncrementCounterAsync();
            await proxy1.IncrementCounterAsync();
            var score2 = await proxy1.IncrementCounterAsync();

            Console.WriteLine($"Current score1: {score1}");
            Console.WriteLine($"Current score2...: {score2}");
        }
    }
}