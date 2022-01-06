using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.PublicUsers.Queries.Details;
using Forum.Application.PublicUsers.PublicUsers.Queries.Posts;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Domain.PublicUsers.Models.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.PublicUsers
{
    public interface IPublicUserQueryRepository : IQueryRepository<PublicUser>
    {
        Task<PublicUserDetailsOutputModel> GetDetails(
            int id,
            CancellationToken cancellationToken = default);

        Task<PublicUserOutputModel> GetDetailsByPostId(
            int postId,
            CancellationToken cancellationToken = default);

        Task<MessageOutputModel> GetMessageDetails(
            int messageId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<MessageOutputModel>> GetInboxMessages(
            int id,
            int skip,
            int take,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<GetPublicUserPostOutputModel>> GetPosts(
           int id,
           int skip,
           int take,
           CancellationToken cancellationToken = default);
    }
}
