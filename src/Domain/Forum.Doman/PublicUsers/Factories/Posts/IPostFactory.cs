using Forum.Domain.Common;
using Forum.Doman.PublicUsers.Models.Posts;

namespace Forum.Domain.PublicUsers.Factories.Posts
{
    public interface IPostFactory : IFactory<Post>
    {
        IPostFactory WithDescription(string description);

        IPostFactory WithImageUrl(string imageUrl);

        IPostFactory WithCategory(string name, string description);

        IPostFactory WithCategory(Category category);
    }
}
