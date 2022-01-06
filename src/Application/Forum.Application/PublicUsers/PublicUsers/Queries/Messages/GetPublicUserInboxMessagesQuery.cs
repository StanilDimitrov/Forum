using Forum.Application.Common;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.PublicUsers.Queries.Messages
{
    public class GetPublicUserInboxMessagesQuery : EntityCommand<int>, IRequest<IEnumerable<MessageOutputModel>>
    {
        private const int MessagesPerPage = 10;

        public int Page { get; set; } = 1;

        public class GetPublicUserInboxMessagesHandler :
            IRequestHandler<GetPublicUserInboxMessagesQuery,
            IEnumerable<MessageOutputModel>>

        {
            private readonly IPublicUserQueryRepository publicUserRepository;

            public GetPublicUserInboxMessagesHandler(IPublicUserQueryRepository publicUserRepository)
               => this.publicUserRepository = publicUserRepository;
            

            public async Task<IEnumerable<MessageOutputModel>> Handle(
                GetPublicUserInboxMessagesQuery request,
                CancellationToken cancellationToken)
            {
                var skip = (request.Page - 1) * MessagesPerPage;

                return await this.publicUserRepository.GetInboxMessages(
                    request.Id, 
                    skip,
                    MessagesPerPage,
                    cancellationToken);
            }
        }
    }
}
