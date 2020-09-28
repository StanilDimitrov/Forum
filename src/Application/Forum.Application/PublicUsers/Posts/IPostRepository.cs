using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Queries.Details;
using Forum.Application.PublicUsers.Posts.Queries.Categories;
using Forum.Application.PublicUsers.Posts.Queries.Common;
using Forum.Application.PublicUsers.Posts.Queries.Details;
using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> Find(int id, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<Category> GetCategory(
            int categoryId,
            CancellationToken cancellationToken = default);

        Task<Comment> GetComment(
           int commentId,
           CancellationToken cancellationToken = default);

        Task<Like> GetLike(
           int likeId,
           CancellationToken cancellationToken = default);

        Task<IEnumerable<GetPostCommentOutputModel>> GetPostComments(
           int id, 
           CancellationToken cancellationToken = default);

        Task<CommentDetailsOutputModel> GetCommentDetails(
          int commentId,
          CancellationToken cancellationToken = default);

        Task<PostOutputModel> GetDetailsByCommentId(
            int commentId,
            CancellationToken cancellationToken = default);

        Task<Post> GetPostByCommentId(
            int commentId,
            CancellationToken cancellationToken = default);

        Task<Post> GetPostByLikeId(
            int likeId,
            CancellationToken cancellationToken = default);

        Task<bool> CheckIsPostLikedByUser(
            int id,
            string userId,
            CancellationToken cancellationToken = default);

        Task<PostDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<GetPostCategoryOutputModel>> GetPostCategories(
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
