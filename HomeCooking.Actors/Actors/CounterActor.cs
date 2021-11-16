using System;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;
using HomeCooking.Domain.Actors;

namespace HomeCooking.Actors.Actors
{
    public class CounterActor : Actor, ICounterActor
    {
        public CounterActor(ActorHost host) : base(host)
        {
            Console.Write(@"Setting up host: {host.Id}");
        }

        public Task<int> IncrementCounterAsync()
        {
            Console.Write("updating counter...");
            return StateManager.AddOrUpdateStateAsync(
                "counter",
                1,
                (key, currentCounter) => currentCounter + 1
            );
        }

        public async Task<int> GetCounterAsync()
        {
            var counterValue = await StateManager.TryGetStateAsync<int>("counter");
            return counterValue.HasValue ? counterValue.Value : 0;
        }
    }
}