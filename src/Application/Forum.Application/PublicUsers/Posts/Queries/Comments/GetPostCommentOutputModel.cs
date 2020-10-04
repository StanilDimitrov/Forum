using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Posts;
using System;

namespace Forum.Application.PublicUsers.Posts.Queries.Categories
{
    public class GetPostCommentOutputModel : IMapFrom<Comment>
    {
        public int Id { get; private set; }

        public string Description { get; private set; } = default!;

        public DateTime CreatedOn { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
          => mapper
              .CreateMap<Comment, GetPostCommentOutputModel>();
    }
}
