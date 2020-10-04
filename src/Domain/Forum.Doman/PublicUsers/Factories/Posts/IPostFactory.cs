using Forum.Domain.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Domain.PublicUsers.Factories.Posts
{
    public interface IPostFactory : IFactory<Post>
    {
        IPostFactory WithDescription(string description);

        IPostFactory WithCategory(string name, string description);

        IPostFactory WithCategory(Category category);
    }
}
