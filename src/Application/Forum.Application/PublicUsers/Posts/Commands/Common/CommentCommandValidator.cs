using FluentValidation;
using Forum.Application.Common;
using System;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;

namespace Forum.Application.PublicUsers.Posts.Commands.Common
{
    public class CommentCommandValidator<TCommand> : AbstractValidator<PostCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public CommentCommandValidator()
        {
            this.RuleFor(c => c.Description)
                .MinimumLength(MinDescriptionLength)
                .MaximumLength(MaxDescriptionLength)
                .NotEmpty();

            this.RuleFor(c => c.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("'{PropertyName}' must be a valid url.")
                .NotEmpty();
        }
    }
}
