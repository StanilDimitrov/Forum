using AutoMapper;
using Forum.Application.Common;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Doman.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Queries.Details

{
    public class InboxMessageDetailsQuery : EntityCommand<int>, IRequest<MessageOutputModel>
    {
        public class InboxMessageDetailsQueryHandler : IRequestHandler<InboxMessageDetailsQuery, MessageOutputModel>
        {
            private readonly IPublicUserDomainRepository userRepository;
            private readonly IMapper mapper;

            public InboxMessageDetailsQueryHandler(
                IPublicUserDomainRepository userRepository,
                IMapper mapper)
            {
                this.userRepository = userRepository;
                this.mapper = mapper;
            }

            public async Task<MessageOutputModel> Handle(
                InboxMessageDetailsQuery request,
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
