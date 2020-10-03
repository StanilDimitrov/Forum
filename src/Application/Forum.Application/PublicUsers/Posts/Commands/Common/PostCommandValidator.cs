using FluentValidation;
using Forum.Application.Common;
using Forum.Doman.PublicUsers.Repositories;
using System;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Post;

namespace Forum.Application.PublicUsers.Posts.Commands.Common
{
    public class PostCommandValidator<TCommand> : AbstractValidator<PostCommand<TCommand>> 
        where TCommand : EntityCommand<int>
    {
        public PostCommandValidator(IPostDomainRepository postRepository)
        {
            this.RuleFor(c => c.Description)
                .MinimumLength(MinDescriptionLength)
                .MaximumLength(MaxDescriptionLength)
                .NotEmpty();

            this.RuleFor(c => c.Category)
                .MustAsync(async (category, token) => await postRepository
                    .GetCategory(category, token) != null)
                .WithMessage("'{PropertyName}' does not exist.");

            this.RuleFor(c => c.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("'{PropertyName}' must be a valid url.")
                .NotEmpty();
        }
    }
}
