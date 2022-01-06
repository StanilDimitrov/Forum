using Forum.Domain.Common;
using Forum.Domain.PublicUsers.Models.Posts;

namespace Forum.Domain.PublicUsers.Factories.Posts
{
    public interface IPostFactory : IFactory<Post>
    {
        IPostFactory WithDescription(string description);

        IPostFactory WithCategory(string name, string description);

        IPostFactory WithCategory(Category category);
    }
}
