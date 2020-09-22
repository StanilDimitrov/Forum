using AutoMapper;
using Forum.Application.Common.Mapping;
using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Application.PublicUsers.Users.Queries.Common
{
    public class UserOutputModel : IMapFrom<User>
    {
        public int Id { get; private set; }

        public string UserName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<User, UserOutputModel>();
    }
}
