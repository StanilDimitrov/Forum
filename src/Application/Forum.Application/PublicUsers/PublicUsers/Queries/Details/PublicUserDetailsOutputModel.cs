using AutoMapper;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Domain.PublicUsers.Models.Users;

namespace Forum.Application.PublicUsers.PublicUsers.Queries.Details
{
    public class PublicUserDetailsOutputModel : PublicUserOutputModel
    {
        public int TotalPosts{ get; private set; }

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<PublicUser, PublicUserDetailsOutputModel>()
                .IncludeBase<PublicUser, PublicUserOutputModel>()
                .ForMember(d => d.TotalPosts, cfg => cfg
                    .MapFrom(d => d.Posts.Count));
    }
}
