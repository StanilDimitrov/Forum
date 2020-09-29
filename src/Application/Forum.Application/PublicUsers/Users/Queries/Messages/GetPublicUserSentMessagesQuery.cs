﻿using AutoMapper;
using Forum.Application.Common;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Users.Queries.Messages
{
    public class GetPublicUserSentMessagesQuery : EntityCommand<int>, IRequest<IEnumerable<MessageOutputModel>>
    {
        private const int MessagesPerPage = 10;

        public int Page { get; set; } = 1;

        public class GetPublicUserSentMessagesQueryHandler :
            IRequestHandler<GetPublicUserSentMessagesQuery,
            IEnumerable<MessageOutputModel>>
        {
            private readonly IPublicUserRepository publicUserRepository;
            private readonly IMapper mapper;

            public GetPublicUserSentMessagesQueryHandler(
                IPublicUserRepository publicUserRepository,
                IMapper mapper)
            {
                this.publicUserRepository = publicUserRepository;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<MessageOutputModel>> Handle(
                GetPublicUserSentMessagesQuery request,
                CancellationToken cancellationToken)
            {
                var user = await this.publicUserRepository.Find(request.Id);

                var skip = (request.Page - 1) * MessagesPerPage;

                var paginatedMessages = user
                    .GetAllSentMessages()
                    .Skip(skip)
                    .Take(MessagesPerPage);

                var messagesOutputModel = mapper.Map<IEnumerable<MessageOutputModel>>(paginatedMessages);

                return messagesOutputModel;
            }
        }
    }
}
