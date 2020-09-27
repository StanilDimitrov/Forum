using Forum.Application.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Queries.Categories
{
    public class GetPostCommentsQuery : EntityCommand<int>, IRequest<IEnumerable<GetPostCommentOutputModel>>
    {
        public class GetPostCommnentsQueryHandler : IRequestHandler<
            GetPostCommentsQuery, 
            IEnumerable<GetPostCommentOutputModel>>
        {
            private readonly IPostRepository postRepository;

            public GetPostCommnentsQueryHandler(IPostRepository postRepository) 
                => this.postRepository = postRepository;

            public async Task<IEnumerable<GetPostCommentOutputModel>> Handle(
                GetPostCommentsQuery request,
                CancellationToken cancellationToken)
                => await this.postRepository.GetPostComments(request.Id, cancellationToken);
        }
    }
}
