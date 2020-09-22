using Forum.Domain.Common;
using System.Threading.Tasks;

namespace Forum.Application.Common
{
    public interface IEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
