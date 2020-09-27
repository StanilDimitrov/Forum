using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Posts;
using System;

namespace Forum.Application.PublicUsers.Posts.Queries.Categories
{
    public class GetPostCommentOutputModel : IMapFrom<Comment>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public DateTime CreatedOn { get; private set; } = default!;

    }
}
