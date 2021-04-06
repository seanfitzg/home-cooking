using System.Threading.Tasks;
using HomeCooking.Api.EventBus;
using HomeCooking.Domain.Events;

namespace HomeCooking.Api.Tests
{
    public class FakeEventBus : IEventBus
    {
        public Task Send(string topicName, IEvent @event)
        {
            return Task.Run(() => {});
        }
    }
}