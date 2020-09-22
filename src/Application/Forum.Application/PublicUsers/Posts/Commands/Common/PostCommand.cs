using Forum.Application.Common;
using System;

namespace Forum.Application.PublicUsers.Posts.Commands.Common
{
    public abstract class PostCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Description { get; set; } = default!;

        public int Category { get; set; }

        public string ImageUrl { get; set; } = default!;

        public DateTime CreatedOn { get; set; }

        public bool isVisible { get; set; }
    }
}
