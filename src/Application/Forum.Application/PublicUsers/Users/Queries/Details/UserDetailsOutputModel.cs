using AutoMapper;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Doman.PublicUsers.Models.Users;

namespace CarRentalSystem.Application.Dealerships.Dealers.Queries.Details
{
    public class UserDetailsOutputModel : UserOutputModel
    {
        public int TotalPosts{ get; private set; }

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<PublicUser, UserDetailsOutputModel>()
                .IncludeBase<PublicUser, UserOutputModel>()
                .ForMember(d => d.TotalPosts, cfg => cfg
                    .MapFrom(d => d.Posts.Count));
    }
}
