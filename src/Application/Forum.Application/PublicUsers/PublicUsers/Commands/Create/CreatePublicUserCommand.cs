using Forum.Application.Common.Contracts;
using Forum.Domain.PublicUsers.Factories.Users;
using Forum.Domain.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.PublicUsers.Commands.Create
{
    public class CreatePublicUserCommand : IRequest<CreatePublicUserOutputModel>
    {
        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public class CreatePublicUserCommandHandler : IRequestHandler<CreatePublicUserCommand, CreatePublicUserOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserFactory publicUserFactory;
            private readonly IPublicUserDomainRepository publicUserRepository;

            public CreatePublicUserCommandHandler(
                ICurrentUser currentUser,
                IPublicUserFactory publicUserFactory,
                IPublicUserDomainRepository publicUserRepository)
            {
                this.currentUser = currentUser;
                this.publicUserFactory = publicUserFactory;
                this.publicUserRepository = publicUserRepository;
            }

            public async Task<CreatePublicUserOutputModel> Handle(
                CreatePublicUserCommand request,
                CancellationToken cancellationToken)
            {
                var publicUser = this.publicUserFactory
                    .WithUserName(request.UserName)
                    .WithEmail(request.Email)
                    .FromUser(this.currentUser.UserId)
                    .Build();

                await this.publicUserRepository.Save(publicUser, cancellationToken);

                return new CreatePublicUserOutputModel(publicUser.Id);
            }
        }
    }
}
