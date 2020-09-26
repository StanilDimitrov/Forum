using Forum.Application.Common;
using System;

namespace Forum.Application.PublicUsers.Comments.Commands.Common
{
    public abstract class CommentCommand<TCommand> : EntityCommand<int>
       where TCommand : EntityCommand<int>
    {
        public string Description { get; set; } = default!;

        public int PostId { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; } = default!;

        public bool IsVisible { get; set; }
    }
}
