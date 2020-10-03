using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Messages.Commands.Common;
using Forum.Doman.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Commands.Create
{
    public class CreateMessageCommand : MessageCommand<CreateMessageCommand>, IRequest<Result>
    {
        public int ReceiverId { get; set; }

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

                var reciever = await this.userRepository.Find(
                   request.ReceiverId,
                   cancellationToken);

                sender.SendMessage(request.Text, reciever);

                await this.userRepository.Save(sender, cancellationToken);
                await this.userRepository.Save(reciever, cancellationToken);

                return Result.Success; ;
            }
        }
    }
}
