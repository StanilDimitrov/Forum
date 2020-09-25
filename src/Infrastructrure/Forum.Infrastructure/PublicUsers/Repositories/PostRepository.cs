using AutoMapper;
using CarRentalSystem.Application.Dealerships.CarAds.Queries.Categories;
using Forum.Application.PublicUsers.Posts;
using Forum.Application.PublicUsers.Posts.Queries.Common;
using Forum.Application.PublicUsers.Posts.Queries.Details;
using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Common;
using Forum.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.PublicUsers.Repositories
{
    internal class PostRepository : DataRepository<IPublicUsersDbContext, Post>, IPostRepository
    {
        private readonly IMapper mapper;

        public PostRepository(IPublicUsersDbContext dbContext, IMapper mapper)
            : base(dbContext)
            => this.mapper = mapper;

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var post = await this.Data.Posts.FindAsync(id);

            if (post == null)
            {
                return false;
            }

            this.Data.Posts.Remove(post);

            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Post> Find(int id, CancellationToken cancellationToken = default)
        => await this
                .All()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken = default)
       => await this
                .Data
                .Categories
                .FirstOrDefaultAsync(p => p.Id == categoryId, cancellationToken);

        public async Task<Comment> GetComment(
           int commentId,
           CancellationToken cancellationToken = default)
           => await this
               .Data
               .Comments
               .FirstOrDefaultAsync(p => p.Id == commentId, cancellationToken);

        public async Task<PostDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
         => await this.mapper
                .ProjectTo<PostDetailsOutputModel>(this.Data.Posts)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IEnumerable<GetPostCategoryOutputModel>> GetPostCategories(CancellationToken cancellationToken = default)
        {
            var posts = await this.mapper
               .ProjectTo<GetPostCategoryOutputModel>(this.Data.Categories)
               .ToDictionaryAsync(k => k.Id, cancellationToken);

            var postsPerCategory = await this.AllPosts()
                .GroupBy(c => c.Category.Id)
                .Select(gr => new
                {
                    CategoryId = gr.Key,
                    TotalPosts = gr.Count()
                })
                .ToListAsync(cancellationToken);

            postsPerCategory.ForEach(c => posts[c.CategoryId].TotalPosts = c.TotalPosts);

            return posts.Values;
        }

        public async Task<IEnumerable<TOutputModel>> GetPostListings<TOutputModel>(
            Specification<Post> postSpecification,
            Specification<PublicUser> publicUserSpecification,
            PostsSortOrder postsSortOrder,
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default)
         => (await this.mapper
               .ProjectTo<TOutputModel>(this
                   .GetPostsQuery(postSpecification, publicUserSpecification)
                   .Sort(postsSortOrder))
               .ToListAsync(cancellationToken))
               .Skip(skip)
               .Take(take);

        public async Task<int> Total(Specification<Post> postSpecification, Specification<PublicUser> publicUserSpecification, CancellationToken cancellationToken = default)
        => await this
                .GetPostsQuery(postSpecification, publicUserSpecification)
                .CountAsync(cancellationToken);

        private IQueryable<Post> AllPosts()
            => this
                .All();

        private IQueryable<Post> GetPostsQuery(
           Specification<Post> postSpecification,
           Specification<PublicUser> publicUserSpecification)
           => this
               .Data
               .PublicUsers
               .Where(publicUserSpecification)
               .SelectMany(d => d.Posts)
               .Where(postSpecification);
    }
}
