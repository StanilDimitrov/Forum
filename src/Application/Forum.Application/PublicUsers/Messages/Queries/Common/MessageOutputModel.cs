using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Users;
using System;

namespace Forum.Application.PublicUsers.Messages.Queries.Common
{
    public class MessageOutputModel : IMapFrom<Message>
    {
        public int Id { get; private set; }

        public string Text { get; private set; } = default!;

        public string SenderUserName { get;  set; } = default!;

        public DateTime CreatedOn { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Message, MessageOutputModel>();
    }
}
