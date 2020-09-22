using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Forum.Application.PublicUsers.Posts;
using MediatR;

namespace CarRentalSystem.Application.Dealerships.CarAds.Queries.Categories
{
    public class GetPostCategoriesQuery : IRequest<IEnumerable<GetPostCategoryOutputModel>>
    {
        public class GetCarAdCategoriesQueryHandler : IRequestHandler<
            GetPostCategoriesQuery, 
            IEnumerable<GetPostCategoryOutputModel>>
        {
            private readonly IPostRepository postRepository;

            public GetCarAdCategoriesQueryHandler(IPostRepository carAdRepository) 
                => this.postRepository = carAdRepository;

            public async Task<IEnumerable<GetPostCategoryOutputModel>> Handle(
                GetPostCategoriesQuery request,
                CancellationToken cancellationToken)
                => await this.postRepository.GetPostCategories(cancellationToken);
        }
    }
}
