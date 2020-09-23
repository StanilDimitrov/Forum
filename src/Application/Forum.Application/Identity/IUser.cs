using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Application.Identity
{
    public interface IUser
    {
        void BecomePublicUser(PublicUser user);
    }
}
