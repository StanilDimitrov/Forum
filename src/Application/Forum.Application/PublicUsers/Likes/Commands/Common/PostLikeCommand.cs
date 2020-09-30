using Forum.Application.Common;

namespace Forum.Application.PublicUsers.Likes.Commands.Common
{
    public abstract class PostLikeCommand<TCommand> 
    {
        public int PostId { get; set; } = default!;

        public bool IsLiked { get; set; } = default!;
    }
}
