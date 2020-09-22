using Forum.Application.PublicUsers.Posts;
using Forum.Domain.PublicUsers.Specifications.Posts;
using Forum.Domain.PublicUsers.Specifications.Users;
using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Queries.Common
{
    public abstract class PostsQuery

    {
        private const int PostsPerPage = 10;

        public string? Manufacturer { get; set; }

        public int? Category { get; set; }

        public string? User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? SortBy { get; set; }

        public string? Order { get; set; }

        public int Page { get; set; } = 1;

        public abstract class CarAdsQueryHandler
        {
            private readonly IPostRepository postRepository;

            protected CarAdsQueryHandler(IPostRepository postRepository)
                => this.postRepository = postRepository;

            protected async Task<IEnumerable<TOutputModel>> GetPostListings<TOutputModel>(
                PostsQuery request,
                bool onlyVisible = true,
                int? userId = default,
                CancellationToken cancellationToken = default)
            {
                var postSpecification = this.GetPostSpecification(request, onlyVisible);

                var userSpecification = this.GetUserSpecification(request, userId);

                var searchOrder = new PostsSortOrder(request.SortBy, request.Order);

                var skip = (request.Page - 1) * PostsPerPage;

                return await this.postRepository.GetPostListings<TOutputModel>(
                    postSpecification,
                    userSpecification,
                    searchOrder,
                    skip,
                    take: PostsPerPage,
                    cancellationToken);
            }

            protected async Task<int> GetTotalPages(
                PostsQuery request,
                bool onlyVisible = true,
                int? userId = default,
                CancellationToken cancellationToken = default)
            {
                var postSpecification = this.GetPostSpecification(request, onlyVisible);

                var userSpecification = this.GetUserSpecification(request, userId);

                var totalPosts = await this.postRepository.Total(
                    postSpecification,
                    userSpecification,
                    cancellationToken);

                return (int)Math.Ceiling((double)totalPosts/PostsPerPage);
            }

            private Specification<Post> GetPostSpecification(PostsQuery request, bool onlyVisible)
                => new PostByCategorySpecification(request.Category)
                    .And(new PostByCreatedOnSpecification(request.StartDate, request.EndDate))
                    .And(new PostOnlyVisibleSpecification(onlyVisible));

            private Specification<User> GetUserSpecification(PostsQuery request, int? userId)
                => new UserByIdSpecification(userId)
                    .And(new UserByUserNameSpecification(request.User));
        }
    }
}
