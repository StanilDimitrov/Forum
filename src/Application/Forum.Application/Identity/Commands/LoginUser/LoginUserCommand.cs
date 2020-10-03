using Forum.Application.Common;
using Forum.Doman.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.Identity.Commands.LoginUser
{
    public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentity identity;
            private readonly IPublicUserDomainRepository publicUserRepository;

            public LoginUserCommandHandler(
                IIdentity identity,
                IPublicUserDomainRepository publicUserRepository)
            {
                this.identity = identity;
                this.publicUserRepository = publicUserRepository;
            }

            public async Task<Result<LoginOutputModel>> Handle(
                LoginUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Login(request);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                var user = result.Data;

                var userId = await this.publicUserRepository.GetPublicUserId(user.UserId, cancellationToken);

                return new LoginOutputModel(user.Token, userId);
            }
        }
    }
}
