using CarRentalSystem.Application.Dealerships.Dealers.Queries.Details;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users.Queries.Details
{
    public class PublicUserDetailsQuery : IRequest<PublicUserDetailsOutputModel>
    {
        public int Id { get; set; }

        public class UserDetailsQueryHandler : IRequestHandler<PublicUserDetailsQuery, PublicUserDetailsOutputModel>
        {
            private readonly IPublicUserRepository userRepository;

            public UserDetailsQueryHandler(IPublicUserRepository userRepository) 
                => this.userRepository = userRepository;

            public async Task<PublicUserDetailsOutputModel> Handle(
                PublicUserDetailsQuery request,
                CancellationToken cancellationToken)
                => await this.userRepository.GetDetails(request.Id, cancellationToken);
        }
    }
}
