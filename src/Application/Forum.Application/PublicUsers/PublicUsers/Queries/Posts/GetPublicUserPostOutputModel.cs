using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Posts;
using System;
using System.Collections.Generic;

namespace Forum.Application.PublicUsers.Users.Queries.Posts
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
