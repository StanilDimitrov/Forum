using Forum.Domain.Common;
using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Domain.PublicUsers.Factories.Users
{
    public interface IUserFactory : IFactory<User>
    {
        IUserFactory WithUserName(string userName);

        IUserFactory WithEmail(string email);
    }
}
