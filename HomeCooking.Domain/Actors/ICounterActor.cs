using System.Threading.Tasks;
using Dapr.Actors;

namespace HomeCooking.Domain.Actors
{
    public interface ICounterActor : IActor
    {
        Task<int> IncrementCounterAsync();

        Task<int> GetCounterAsync();
    }
}