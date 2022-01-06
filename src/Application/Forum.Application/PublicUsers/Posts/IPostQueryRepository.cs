using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Queries.Details;
using Forum.Application.PublicUsers.Posts.Queries.Categories;
using Forum.Application.PublicUsers.Posts.Queries.Common;
using Forum.Application.PublicUsers.Posts.Queries.Details;
using Forum.Domain.PublicUsers.Models.Posts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.PublicUsers.Posts.Queries.Comments;
using Forum.Domain.Common;
using Forum.Domain.PublicUsers.Models.Users;

namespace Forum.Application.PublicUsers.Posts
{
    public interface IPostQueryRepository : IQueryRepository<Post>
    {
        Task<CommentDetailsOutputModel> GetCommentDetails(
          int commentId,
          CancellationToken cancellationToken = default);

        Task<PostOutputModel> GetDetailsByCommentId(
            int commentId,
            CancellationToken cancellationToken = default);

        Task<PostDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<GetPostCategoryOutputModel>> GetPostCategories(
            CancellationToken cancellationToken = default);

        Task<IEnumerable<GetPostCommentOutputModel>> GetPostComments(
            int id,
            int skip,
            int take,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TOutputModel>> GetPostListings<TOutputModel>(
           Specification<Post> postSpecification,
           Specification<PublicUser> userSpecification,
           PostsSortOrder postsSortOrder,
           int skip = 0,
           int take = int.MaxValue,
           CancellationToken cancellationToken = default);

        Task<int> Total(
           Specification<Post> postSpecification,
           Specification<PublicUser> publicUserSpecification,
           CancellationToken cancellationToken = default);
    }
}
