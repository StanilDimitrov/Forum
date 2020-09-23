using Forum.Domain.Common;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Common.Events
{
    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
