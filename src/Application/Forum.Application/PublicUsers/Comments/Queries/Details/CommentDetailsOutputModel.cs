using AutoMapper;
using Forum.Application.PublicUsers.Comments.Queries.Common;
using Forum.Domain.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Comments.Queries.Details
{
    public class CommentDetailsOutputModel : CommentOutputModel
    {
        public string UserName { get; set; } = default!;

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Comment, CommentDetailsOutputModel>()
                .IncludeBase<Comment, CommentOutputModel>()
            .ForMember(pu => pu.UserName, cfg => cfg
                    .MapFrom(ad => ad.PublicUser.UserName));
    }

}
