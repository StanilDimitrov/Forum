using Forum.Domain.Common;
using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Domain.PublicUsers.Factories.Users
{
    public interface IPublicUserFactory : IFactory<PublicUser>
    {
        IPublicUserFactory WithUserName(string userName);

        IPublicUserFactory WithEmail(string email);

        IPublicUserFactory FromUser(string userId);
    }
}
