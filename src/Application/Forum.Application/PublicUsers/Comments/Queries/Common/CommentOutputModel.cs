using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Comments.Queries.Common
{
    public class CommentOutputModel : IMapFrom<Comment>
    {
        public int Id { get; private set; }

        public string Description { get; private set; } = default!;

        public string CreatedOn { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
           => mapper
               .CreateMap<Comment, CommentOutputModel>();

    }
}
