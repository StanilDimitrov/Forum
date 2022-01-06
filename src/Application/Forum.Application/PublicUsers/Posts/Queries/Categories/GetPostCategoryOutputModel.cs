using Forum.Application.Common.Mapping;
using Forum.Domain.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Posts.Queries.Categories
{
    public class GetPostCategoryOutputModel : IMapFrom<Category>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public int TotalPosts { get; set; }
    }
}
