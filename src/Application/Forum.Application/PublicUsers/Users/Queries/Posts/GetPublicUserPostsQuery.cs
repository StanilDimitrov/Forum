using Forum.Application.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users.Queries.Posts
{
    public class GetPublicUserPostsQuery : EntityCommand<int>, IRequest<IEnumerable<GetPublicUserPostOutputModel>>
    {
        public class GetPublicUserPostQueryHandler : IRequestHandler<
            GetPublicUserPostsQuery,
            IEnumerable<GetPublicUserPostOutputModel>>
        {
            private readonly IPublicUserRepository publicUserRepository;

            public GetPublicUserPostQueryHandler(IPublicUserRepository publicUserRepository)
                => this.publicUserRepository = publicUserRepository;

            public async Task<IEnumerable<GetPublicUserPostOutputModel>> Handle(
                GetPublicUserPostsQuery request,
                CancellationToken cancellationToken)
                => await this.publicUserRepository.GetPublicUserPosts(request.Id, cancellationToken);
        }
    }
}
