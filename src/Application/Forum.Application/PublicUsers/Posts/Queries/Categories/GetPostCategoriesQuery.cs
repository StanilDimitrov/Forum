using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Queries.Categories
{
    public class GetPostCategoriesQuery : IRequest<IEnumerable<GetPostCategoryOutputModel>>
    {
        public class GetPostCategoriesQueryHandler : IRequestHandler<
            GetPostCategoriesQuery, 
            IEnumerable<GetPostCategoryOutputModel>>
        {
            private readonly IPostQueryRepository postRepository;

            public GetPostCategoriesQueryHandler(IPostQueryRepository carAdRepository) 
                => this.postRepository = carAdRepository;

            public async Task<IEnumerable<GetPostCategoryOutputModel>> Handle(
                GetPostCategoriesQuery request,
                CancellationToken cancellationToken)
                => await this.postRepository.GetPostCategories(cancellationToken);
        }
    }
}
