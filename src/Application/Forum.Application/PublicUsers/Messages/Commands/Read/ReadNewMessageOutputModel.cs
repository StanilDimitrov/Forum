using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Doman.PublicUsers.Models.Users;
using System;

namespace Forum.Application.PublicUsers.Messages.Commands.Read
{
    public class ReadNewMessageOutputModel : IMapFrom<Message>
    {
        public int Id { get; private set; }

        public PublicUserOutputModel User { get; set; } = default!;

        public string Text { get; private set; } = default!;

        public DateTime CreatedOn { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Message, ReadNewMessageOutputModel>();
    }
}
