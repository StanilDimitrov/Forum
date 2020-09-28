﻿using AutoMapper;
using Forum.Application.PublicUsers.Posts.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Doman.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Posts.Queries.Details
{
    public class PostDetailsOutputModel : PostOutputModel
    {
        public PublicUserOutputModel User { get; set; } = default!;

        public int TotalLikes { get; set; }

        public int TotalDislikes { get; set; }

        public override void Mapping(Profile mapper) 
            => mapper
                .CreateMap<Post, PostDetailsOutputModel>()
                .IncludeBase<Post, PostOutputModel>();
                
    }
}
