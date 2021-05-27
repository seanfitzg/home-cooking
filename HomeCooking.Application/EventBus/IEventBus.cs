using System.Threading.Tasks;
using HomeCooking.Domain.Events;

namespace HomeCooking.Application.EventBus
{
    public interface IEventBus
    {
        Task Send(string topicName, IEvent @event);
    }
}