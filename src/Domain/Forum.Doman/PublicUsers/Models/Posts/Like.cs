using Forum.Domain.Common.Models;

namespace Forum.Doman.PublicUsers.Models.Posts
{
    public class Like : Entity<int>
    {
        internal Like( bool isLiked, string userId)
        {
            this.IsLike = isLiked;
            this.UserId = userId;
        }

        public string? UserId { get; private set; }

        public bool IsLike { get; private set; }

        public Like ChangeLike()
        {
            this.IsLike = !this.IsLike;

            return this;
        }
    }
}
