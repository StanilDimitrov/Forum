using Forum.Domain.Common;

namespace Forum.Application.Common.Contracts
{
    public interface IQueryRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
    }
}
