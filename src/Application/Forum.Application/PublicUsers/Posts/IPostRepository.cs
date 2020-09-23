using CarRentalSystem.Application.Dealerships.CarAds.Queries.Categories;
using Forum.Application.Common.Contracts;
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
           Specification<Post> carAdSpecification,
           Specification<PublicUser> userSpecification,
           CancellationToken cancellationToken = default);
    }
}
