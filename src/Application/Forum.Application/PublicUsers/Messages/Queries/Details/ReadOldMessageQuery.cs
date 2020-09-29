using AutoMapper;
using Forum.Application.Common;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Queries.Details

{
    public class ReadOldMessageQuery : EntityCommand<int>, IRequest<MessageOutputModel>
    {
        public class ReadOldMessageQueryHandler : IRequestHandler<ReadOldMessageQuery, MessageOutputModel>
        {
            private readonly IPublicUserRepository userRepository;
            private readonly IMapper mapper;

            public ReadOldMessageQueryHandler(
                IPublicUserRepository userRepository,
                IMapper mapper)
            {
                this.userRepository = userRepository;
                this.mapper = mapper;
            }

            public async Task<MessageOutputModel> Handle(
                ReadOldMessageQuery request,
                CancellationToken cancellationToken)
            {
                var message = await this.userRepository.GetMessage(
                    request.Id,
                    cancellationToken);

                return this.mapper.Map<MessageOutputModel>(message);
            }
        }
    }
}
