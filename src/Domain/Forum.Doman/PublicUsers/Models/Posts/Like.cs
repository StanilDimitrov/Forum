namespace Forum.Domain.PublicUsers.Models.Posts
{
    public class Like
    {
        internal Like(bool isLike, string userId)
        {
            this.IsLike = isLike;
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
