using Forum.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.PublicUsers.Queries.Details
{
    public class PublicUserDetailsQuery : EntityCommand<int>, IRequest<PublicUserDetailsOutputModel>
    {
        public class UserDetailsQueryHandler : IRequestHandler<PublicUserDetailsQuery, PublicUserDetailsOutputModel>
        {
            private readonly IPublicUserQueryRepository userRepository;

            public UserDetailsQueryHandler(IPublicUserQueryRepository userRepository) 
                => this.userRepository = userRepository;

            public async Task<PublicUserDetailsOutputModel> Handle(
                PublicUserDetailsQuery request,
                CancellationToken cancellationToken)
                => await this.userRepository.GetDetails(request.Id, cancellationToken);
        }
    }
}
