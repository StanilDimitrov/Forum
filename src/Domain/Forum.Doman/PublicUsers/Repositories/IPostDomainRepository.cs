using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Doman.PublicUsers.Repositories
{
    public interface IPostDomainRepository : IDomainRepository<Post>
    {
        Task<Post> Find(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<Category> GetCategory(
            int categoryId,
            CancellationToken cancellationToken = default);

        Task<Comment> GetComment(
           int commentId,
           CancellationToken cancellationToken = default);

        Task<Post> GetPostByCommentId(
            int commentId,
            CancellationToken cancellationToken = default);

        Task<bool> CheckIsPostLikedByUser(
            int id,
            string userId,
            CancellationToken cancellationToken = default);
    }
}

