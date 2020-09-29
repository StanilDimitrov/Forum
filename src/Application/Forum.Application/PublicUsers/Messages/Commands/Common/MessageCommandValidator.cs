using FluentValidation;
using Forum.Application.Common;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Message;

namespace Forum.Application.PublicUsers.Messages.Commands.Common
{
    public class MessageCommandValidator<TCommand> : AbstractValidator<MessageCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public MessageCommandValidator()
        {
            this.RuleFor(c => c.Text)
                .MinimumLength(MinTextLenght)
                .MaximumLength(MaxTextLength)
                .NotEmpty();
        }
    }
}
