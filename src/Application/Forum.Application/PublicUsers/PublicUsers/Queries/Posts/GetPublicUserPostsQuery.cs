using AutoMapper;
using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users.Queries.Posts
{
    public class GetPublicUserPostsQuery : EntityCommand<int>, IRequest<IEnumerable<GetPublicUserPostOutputModel>>
    {
        private const int PostsPerPage = 10;

        public int Page { get; set; } = 1;

        public class GetPublicUserPostQueryHandler : IRequestHandler<
            GetPublicUserPostsQuery,
            IEnumerable<GetPublicUserPostOutputModel>>
        {
            private readonly IPublicUserRepository publicUserRepository;
            private readonly ICurrentUser currentUser;
            private readonly IMapper mapper;

            public GetPublicUserPostQueryHandler(
                IPublicUserRepository publicUserRepository,
                ICurrentUser currentUser,
                IMapper mapper)
            {
                this.publicUserRepository = publicUserRepository;
                this.currentUser = currentUser;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<GetPublicUserPostOutputModel>> Handle(
                GetPublicUserPostsQuery request,
                CancellationToken cancellationToken)
            {
                var publicUser = await this.publicUserRepository.FindByCurrentUser(currentUser.UserId, cancellationToken);

                var skip = (request.Page - 1) * PostsPerPage;

                var posts =
                    publicUser.GetAllPosts()
                    .Skip(skip)
                    .Take(PostsPerPage);

                return this.mapper.Map<IEnumerable<GetPublicUserPostOutputModel>>(posts);
            }
                
        }
    }
}
