using Forum.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.Common.Contracts
{
    public interface IRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
        Task Save(TEntity entity, CancellationToken cancellationToken = default);
    }
}
