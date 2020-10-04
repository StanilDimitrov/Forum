using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Posts;
using System;

namespace Forum.Application.PublicUsers.Posts.Queries.Common
{
    public class PostOutputModel : IMapFrom<Post>
    {
        public int Id { get; private set; }

        public string Description { get; private set; } = default!;

        public string Category { get; private set; } = default!;

        public DateTime CreatedOn { get; set; } = default!;

        public virtual void Mapping(Profile mapper) 
            => mapper
                .CreateMap<Post, PostOutputModel>()
                .ForMember(ad => ad.Category, cfg => cfg
                    .MapFrom(ad => ad.Category.Name));
    }
}
