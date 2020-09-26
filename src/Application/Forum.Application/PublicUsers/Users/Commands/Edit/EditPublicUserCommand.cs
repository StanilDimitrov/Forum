using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users.Commands.Edit
{
    public class EditPublicUserCommand : EntityCommand<int>, IRequest<Result>
    {
        public string User { get; set; } = default!;

        public string Email { get; set; } = default!;

        public class EditUserCommandHandler : IRequestHandler<EditPublicUserCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserRepository userRepository;

            public EditUserCommandHandler(
                ICurrentUser currentUser,
                IPublicUserRepository userRepository)
            {
                this.currentUser = currentUser;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                EditPublicUserCommand request, 
                CancellationToken cancellationToken)
            {
                var user = await this.userRepository.FindByCurrentUser(
                    this.currentUser.UserId, 
                    cancellationToken);

                if (request.Id != user.Id)
                {
                    return "You cannot edit this public user.";
                }

                user.UpdateEmail(request.Email);

                await this.userRepository.Save(user, cancellationToken);

                return Result.Success;
            }
        }
    }
}
