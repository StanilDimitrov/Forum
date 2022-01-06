using Forum.Application.Common;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.PublicUsers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Messages.Queries.Details

{
    public class InboxMessageDetailsQuery : EntityCommand<int>, IRequest<MessageOutputModel>
    {
        public class InboxMessageDetailsQueryHandler : IRequestHandler<InboxMessageDetailsQuery, MessageOutputModel>
        {
            private readonly IPublicUserQueryRepository userRepository;

            public InboxMessageDetailsQueryHandler(IPublicUserQueryRepository userRepository)
              =>  this.userRepository = userRepository;
            

            public async Task<MessageOutputModel> Handle(
                InboxMessageDetailsQuery request,
                CancellationToken cancellationToken)
            {
                return await this.userRepository.GetMessageDetails(request.Id, cancellationToken);
            }
        }
    }
}
