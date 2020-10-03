using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Doman.PublicUsers.Repositories
{
    public interface IPublicUserDomainRepository : IDomainRepository<PublicUser>
    {
        Task<PublicUser> Find(
            int id,
            CancellationToken cancellationToken = default);

        Task<PublicUser> FindByCurrentUser(
            string userId,
            CancellationToken cancellationToken = default);

        Task<int> GetPublicUserId(
            string userId,
            CancellationToken cancellationToken = default);

        Task<bool> HasPost(
            int publicUserId,
            int postId,
            CancellationToken cancellationToken = default);

        Task<bool> HasMessage(
            int publicUserId,
            int messageId,
            CancellationToken cancellationToken = default);

        Task<Message> GetMessage(
            int messageId,
            CancellationToken cancellationToken = default);
    }
}
