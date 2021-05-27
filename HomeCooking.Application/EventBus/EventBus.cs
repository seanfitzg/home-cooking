using System;
using System.Threading.Tasks;
using Dapr.Client;
using HomeCooking.Domain.Events;

namespace HomeCooking.Application.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly DaprClient _daprClient;

        public EventBus(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public async Task Send(string topicName, IEvent @event)
        {
            try
            {
                Console.WriteLine($@"Publishing {topicName}, {@event}");
                await _daprClient.PublishEventAsync("pubsub-homecooking", topicName, (dynamic)@event);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}