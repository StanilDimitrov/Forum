using Forum.Application.Common;
using Forum.Application.PublicUsers.Users;
using Forum.Domain.PublicUsers.Factories.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.Identity.Commands.CreateUser
{
    public class CreateUserCommand : UserInputModel, IRequest<Result>
    {
        public string UserName { get; set; } = default!;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
        {
            private readonly IIdentity identity;
            private readonly IPublicUserFactory publicUserFactory;
            private readonly IPublicUserRepository publicUserRepository;

            public CreateUserCommandHandler(
                IIdentity identity,
                IPublicUserFactory publicUserFactory,
                IPublicUserRepository publicUserRepository)
            {
                this.identity = identity;
                this.publicUserFactory = publicUserFactory;
                this.publicUserRepository = publicUserRepository;
            }

            public async Task<Result> Handle(
                CreateUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Register(request);

                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                var publicUser = this.publicUserFactory
                    .WithUserName(request.UserName)
                    .WithEmail(request.Email)
                    .Build();

                user.BecomePublicUser(publicUser);

                await this.publicUserRepository.Save(publicUser, cancellationToken);

                return result;
            }
        }
    }
}