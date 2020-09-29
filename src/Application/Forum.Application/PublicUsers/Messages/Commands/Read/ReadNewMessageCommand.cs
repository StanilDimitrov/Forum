using AutoMapper;
using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Users;
using Forum.Doman.PublicUsers.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Commands.Read
{
    public class ReadNewMessageCommand : EntityCommand<int>, IRequest<ReadNewMessageOutputModel>
    {
        public class ReadNewMessageDetailsCommandHandler : IRequestHandler<ReadNewMessageCommand, ReadNewMessageOutputModel>
        {
            private readonly IPublicUserRepository userRepository;
            private readonly ICurrentUser currentUser;
            private readonly IMapper mapper;

            public ReadNewMessageDetailsCommandHandler(
                IPublicUserRepository userRepository,
                ICurrentUser currentUser,
                IMapper mapper)
            {
                this.userRepository = userRepository;
                this.currentUser = currentUser;
                this.mapper = mapper;
            }

            public async Task<ReadNewMessageOutputModel> Handle(
                ReadNewMessageCommand request,
                CancellationToken cancellationToken)
            {
                var message = await this.userRepository.GetMessage(
                    request.Id,
                    cancellationToken);

                var user = await this.userRepository.FindByCurrentUser(currentUser.UserId);
                if (user.Id != message.Reciever.Id)
                {
                    throw new InvalidPublicUserException("You are not authorized to read this message");
                }

                user.ReadNewMessage(message);
                await this.userRepository.Save(user, cancellationToken);

                return this.mapper.Map<ReadNewMessageOutputModel>(message);
            }
        }
    }
}

