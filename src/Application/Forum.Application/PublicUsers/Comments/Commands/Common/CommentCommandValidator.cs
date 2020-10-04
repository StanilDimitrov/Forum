using FluentValidation;
using Forum.Application.Common;
using System;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;

namespace Forum.Application.PublicUsers.Comments.Commands.Common
{
    public class CommentCommandValidator<TCommand> : AbstractValidator<CommentCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public CommentCommandValidator()
        {
            this.RuleFor(c => c.Description)
                .MinimumLength(MinDescriptionLength)
                .MaximumLength(MaxDescriptionLength)
                .NotEmpty();
        }
    }
}
