using Forum.Application.Common;
using Forum.Application.PublicUsers.Posts;
using Forum.Application.PublicUsers.Posts.Queries.Details;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users.Queries.Common
{
    public class PostDetailsQuery : EntityCommand<int>, IRequest<PostDetailsOutputModel>
    {
        public class PostDetailsQueryHandler : IRequestHandler<PostDetailsQuery, PostDetailsOutputModel>
        {
            private readonly IPostQueryRepository postRepository;
            private readonly IPublicUserQueryRepository userRepository;

            public PostDetailsQueryHandler(
                IPostQueryRepository postRepository,
                IPublicUserQueryRepository userRepository)
            {
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<PostDetailsOutputModel> Handle(
                PostDetailsQuery request, 
                CancellationToken cancellationToken)
            {
                var postDetails = await this.postRepository.GetDetails(
                    request.Id,
                    cancellationToken);

                postDetails.User = await this.userRepository.GetDetailsByPostId(
                    request.Id,
                    cancellationToken);

                return postDetails;
            }
        }
    }
}
