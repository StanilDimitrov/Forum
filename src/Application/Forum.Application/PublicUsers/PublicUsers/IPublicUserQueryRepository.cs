using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Details;
using Forum.Application.PublicUsers.Users.Queries.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users
{
    public interface IPublicUserQueryRepository : IQueryRepository<PublicUser>
    {
        Task<PublicUserDetailsOutputModel> GetDetails(
            int id,
            CancellationToken cancellationToken = default);

        Task<PublicUserOutputModel> GetDetailsByPostId(
            int postId,
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
