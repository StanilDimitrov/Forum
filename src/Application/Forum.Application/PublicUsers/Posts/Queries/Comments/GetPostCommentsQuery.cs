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
            private readonly IPostRepository postRepository;
            private readonly IMapper mapper;

            public GetPostCommnentsQueryHandler(
                IPostRepository postRepository,
                IMapper mapper)
            {
                this.postRepository = postRepository;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<GetPostCommentOutputModel>> Handle(
                GetPostCommentsQuery request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.Id, cancellationToken);

                var skip = (request.Page - 1) * CommentsPerPage;
                var comments = 
                    post.GetComments()
                    .Skip(skip)
                    .Take(CommentsPerPage);

                return this.mapper.Map<IEnumerable<GetPostCommentOutputModel>>(comments);
            }
        }
    }
}
