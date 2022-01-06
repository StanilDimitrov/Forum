using Forum.Application.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.PublicUsers.PublicUsers;
using Forum.Application.PublicUsers.PublicUsers.Queries.Posts;

namespace Forum.Application.PublicUsers.PublicUsers.Queries.Posts
{
    public class GetPublicUserPostsQuery : EntityCommand<int>, IRequest<IEnumerable<GetPublicUserPostOutputModel>>
    {
        private const int PostsPerPage = 10;

        public int Page { get; set; } = 1;

        public class GetPublicUserPostQueryHandler : IRequestHandler<
            GetPublicUserPostsQuery,
            IEnumerable<GetPublicUserPostOutputModel>>
        {
            private readonly IPublicUserQueryRepository publicUserRepository;

            public GetPublicUserPostQueryHandler(IPublicUserQueryRepository publicUserRepository)
                 => this.publicUserRepository = publicUserRepository;

            public async Task<IEnumerable<GetPublicUserPostOutputModel>> Handle(
                GetPublicUserPostsQuery request,
                CancellationToken cancellationToken)
            {
                var skip = (request.Page - 1) * PostsPerPage;

                return await this.publicUserRepository.GetPosts(
                    request.Id,
                    skip, PostsPerPage,
                    cancellationToken);
            }
        }
    }
}
