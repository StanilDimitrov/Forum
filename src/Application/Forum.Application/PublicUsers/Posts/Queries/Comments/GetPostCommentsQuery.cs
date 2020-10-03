using AutoMapper;
using Forum.Application.Common;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Queries.Categories
{
    public class GetPostCommentsQuery : EntityCommand<int>, IRequest<IEnumerable<GetPostCommentOutputModel>>
    {
        private const int CommentsPerPage = 10;

        public int Page { get; set; } = 1;

        public class GetPostCommnentsQueryHandler : IRequestHandler<
            GetPostCommentsQuery,
            IEnumerable<GetPostCommentOutputModel>>
        {
            private readonly IPostQueryRepository postRepository;

            public GetPostCommnentsQueryHandler(IPostQueryRepository postRepository)
                => this.postRepository = postRepository;

            public async Task<IEnumerable<GetPostCommentOutputModel>> Handle(
                GetPostCommentsQuery request,
                CancellationToken cancellationToken)
            {
                var skip = (request.Page - 1) * CommentsPerPage;

                return await this.postRepository.GetPostComments(
                    request.Id,
                    skip,
                    CommentsPerPage,
                    cancellationToken);
            }
        }
    }
}
