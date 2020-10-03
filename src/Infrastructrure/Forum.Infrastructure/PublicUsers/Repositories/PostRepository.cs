using AutoMapper;
using Forum.Application.Common.Exceptions;
using Forum.Application.PublicUsers.Comments.Queries.Details;
using Forum.Application.PublicUsers.Posts;
using Forum.Application.PublicUsers.Posts.Queries.Categories;
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

        public async Task<bool> CheckIsPostLikedByUser(
            int id,
            string userId,
            CancellationToken cancellationToken = default)
        {
            var post = await this.Data.Posts.FindAsync(id);

            if (post == null)
            {
                return false;
            }

            var isPostLikedByUser = post.Likes.Any(l => l.UserId == userId);
            return isPostLikedByUser;
        }

        public async Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken = default)
       => await this
                .Data
                .Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        public async Task<Comment> GetComment(
           int commentId,
           CancellationToken cancellationToken = default)
           => await this
               .Data
               .Comments
               .FirstOrDefaultAsync(c => c.Id == commentId, cancellationToken);

        public async Task<CommentDetailsOutputModel> GetCommentDetails(
          int commentId,
          CancellationToken cancellationToken = default)
          => await this.mapper
                .ProjectTo<CommentDetailsOutputModel>(this.Data.Comments)
              .FirstOrDefaultAsync(c => c.Id == commentId, cancellationToken);

        public async Task<IEnumerable<GetPostCommentOutputModel>> GetAllPostComments(
          int id,
          CancellationToken cancellationToken = default)
        {
            var post = await Find(id, cancellationToken);

            var comments = post.Comments.OrderByDescending(x => x.CreatedOn)
             .ToList();
            return this.mapper.Map<IEnumerable<GetPostCommentOutputModel>>(comments);
        }

        public async Task<PostOutputModel> GetDetailsByCommentId(int commentId, CancellationToken cancellationToken = default)
        => await this.mapper
               .ProjectTo<PostDetailsOutputModel>(this
                   .All()
                   .Where(p => p.Comments.Any(c => c.Id == commentId)))
               .SingleOrDefaultAsync(cancellationToken);

        public async Task<Post> GetPostByCommentId(int commentId, CancellationToken cancellationToken = default)
        => await this
               .All()
               .SingleOrDefaultAsync(p => p.Comments.Any(c => c.Id == commentId),
            cancellationToken);

        public async Task<PostDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
        {
            var post = await this.Data.Posts.FindAsync(id);
            if (post == null)
            {
                throw new NotFoundException(nameof(Post), id);
            }

            var postDetailsOutputModel = this.mapper
                .Map<PostDetailsOutputModel>(post);
            var totalLikes = post.Likes.Where(l => l.IsLike).Count();
            var totalDislikes = post.Likes.Count - totalLikes;

            postDetailsOutputModel.TotalLikes = totalLikes;
            postDetailsOutputModel.TotalDislikes = totalDislikes;
            return postDetailsOutputModel;
        }

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
