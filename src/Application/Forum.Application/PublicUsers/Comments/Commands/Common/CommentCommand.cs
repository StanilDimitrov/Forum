using Forum.Application.Common;

namespace Forum.Application.PublicUsers.Comments.Commands.Common
{
    public abstract class CommentCommand<TCommand> : EntityCommand<int>
       where TCommand : EntityCommand<int>
    {
        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;
    }
}
