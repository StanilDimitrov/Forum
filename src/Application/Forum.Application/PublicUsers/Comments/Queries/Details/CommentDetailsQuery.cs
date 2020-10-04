using Forum.Application.Common;
using Forum.Application.PublicUsers.Posts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Comments.Queries.Details
{
    public class CommentDetailsQuery : EntityCommand<int>, IRequest<CommentDetailsOutputModel>
    {
        public class CarAdDetailsQueryHandler : IRequestHandler<CommentDetailsQuery, CommentDetailsOutputModel>
        {
            private readonly IPostQueryRepository postRepository;

            public CarAdDetailsQueryHandler(
                IPostQueryRepository postRepository)
            {
                this.postRepository = postRepository;
            }

            public async Task<CommentDetailsOutputModel> Handle(
                CommentDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var commentDetails = await this.postRepository.GetCommentDetails(
                    request.Id,
                    cancellationToken);

                return commentDetails;
            }
        }
    }
}
