using Forum.Domain.Common;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Common.Events
{
    internal interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}