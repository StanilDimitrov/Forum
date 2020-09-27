﻿using AutoMapper;
using Forum.Application.PublicUsers.Comments.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Doman.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Comments.Queries.Details
{
    public class CommentDetailsOutputModel : CommentOutputModel
    {
        public PublicUserOutputModel User { get; set; } = default!;

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Comment, CommentDetailsOutputModel>()
                .IncludeBase<Comment, CommentOutputModel>();

    }
    
}