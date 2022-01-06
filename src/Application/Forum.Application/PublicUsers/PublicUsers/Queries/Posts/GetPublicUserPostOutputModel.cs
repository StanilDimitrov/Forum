using AutoMapper;
using Forum.Application.Common.Mapping;
using System;
using Forum.Domain.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.PublicUsers.Queries.Posts
{
    public class GetPublicUserPostOutputModel : IMapFrom<Post>
    {
        public int Id { get; private set; }

        public string Description { get; private set; } = default!;

        public DateTime CreatedOn { get; set; }

        public virtual void Mapping(Profile mapper)
           => mapper
               .CreateMap<Post, GetPublicUserPostOutputModel>();
    }
    
}
