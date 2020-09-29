using Forum.Application.Common;

namespace Forum.Application.PublicUsers.Messages.Commands.Common
{
    public abstract class MessageCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Text { get; set; } = default!;
    }
}
