namespace Forum.Doman.PublicUsers.Models.Posts
{
    public class Like
    {
        internal Like( bool isLiked, string userId)
        {
            this.IsLiked = isLiked;
            this.UserId = userId;
        }

        public string? UserId { get; private set; }

        public bool IsLiked { get; private set; }
    }
}
