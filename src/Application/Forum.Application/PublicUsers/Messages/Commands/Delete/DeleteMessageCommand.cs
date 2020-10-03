using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Doman.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Commands.Delete
{
    public class DeleteMessageCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserDomainRepository userRepository;

            public DeleteMessageCommandHandler(
                ICurrentUser currentUser,
                IPublicUserDomainRepository userRepository)
            {
                this.currentUser = currentUser;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                DeleteMessageCommand request,
                CancellationToken cancellationToken)
            {
                var user = await this.userRepository.FindByCurrentUser(currentUser.UserId);
                var message = await this.userRepository.GetMessage(request.Id);
                var userHasMessage = await this.userRepository.HasMessage(user.Id, request.Id);

                if (!userHasMessage)
                {
                    return userHasMessage;
                }

                user.DeleteMessage(message);
                await this.userRepository.Save(user, cancellationToken);

                return Result.Success;
            }
        }
    }
}
