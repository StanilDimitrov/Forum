using Forum.Application.Common;
using Forum.Application.PublicUsers.Posts;
using Forum.Application.PublicUsers.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Comments.Queries.Details
{
    public class CommentDetailsQuery : EntityCommand<int>, IRequest<CommentDetailsOutputModel>
    {
        public int PostId { get; set; }

        public class CarAdDetailsQueryHandler : IRequestHandler<CommentDetailsQuery, CommentDetailsOutputModel>
        {
            private readonly IPostQueryRepository postRepository;
            private readonly IPublicUserQueryRepository userRepository;

            public CarAdDetailsQueryHandler(
                IPostQueryRepository postRepository,
                IPublicUserQueryRepository userRepository)
            {
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<CommentDetailsOutputModel> Handle(
                CommentDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var commentDetails = await this.postRepository.GetCommentDetails(
                    request.Id,
                    cancellationToken);

                commentDetails.User = await this.userRepository.GetDetailsByPostId(
                    request.PostId,
                    cancellationToken);

                return commentDetails;
            }
        }
    }
}
