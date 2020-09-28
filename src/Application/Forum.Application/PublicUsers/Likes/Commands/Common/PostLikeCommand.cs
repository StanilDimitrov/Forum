using Forum.Application.Common;

namespace Forum.Application.PublicUsers.Likes.Commands.Common
{
    public abstract class PostLikeCommand<TCommand> : EntityCommand<int>
       where TCommand : EntityCommand<int>
    {
        public bool IsLiked { get; set; } = default!;
    }
}
