using AutoMapper;
using Forum.Application.PublicUsers.Posts.Queries.Common;
using Forum.Domain.PublicUsers.Models;
using Forum.Domain.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Posts.Queries.Details
{
    public class PostDetailsOutputModel : PostOutputModel
    {
        public string UserName { get; set; } = default!;

        public int TotalLikes { get; set; }

        public int TotalDislikes { get; set; }

        public override void Mapping(Profile mapper) 
            => mapper
                .CreateMap<Post, PostDetailsOutputModel>()
                .IncludeBase<Post, PostOutputModel>()
             .ForMember(p => p.Category, cfg => cfg
                    .MapFrom(ad => ad.Category.Name));

    }
}
