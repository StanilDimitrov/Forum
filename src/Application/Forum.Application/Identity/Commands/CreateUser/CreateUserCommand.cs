using Forum.Application.Common;
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

            public CreateUserCommandHandler(IIdentity identity)
                => this.identity = identity;

            public async Task<Result> Handle(
                CreateUserCommand request,
                CancellationToken cancellationToken)
                => await this.identity.Register(request);
        }
    }
}