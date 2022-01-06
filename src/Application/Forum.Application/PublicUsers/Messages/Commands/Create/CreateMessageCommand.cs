using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Messages.Commands.Common;
using Forum.Domain.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Commands.Create
{
    public class CreateMessageCommand : MessageCommand<CreateMessageCommand>, IRequest<Result>
    {
        public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserDomainRepository userRepository;

            public CreateMessageCommandHandler(
                ICurrentUser currentUser, 
                IPublicUserDomainRepository userRepository)
            {
                this.currentUser = currentUser;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                CreateMessageCommand request, 
                CancellationToken cancellationToken)
            {
                var sender = await this.userRepository.FindByCurrentUser(
                    this.currentUser.UserId, 
                    cancellationToken);

                var receiver = await this.userRepository.Find(
                   request.Id,
                   cancellationToken);

                if (sender == receiver)
                {
                    return false;
                }

                sender.SendMessage(request.Text, receiver, currentUser.UserId);

                await this.userRepository.Save(receiver, cancellationToken);

                return Result.Success; ;
            }
        }
    }
}
