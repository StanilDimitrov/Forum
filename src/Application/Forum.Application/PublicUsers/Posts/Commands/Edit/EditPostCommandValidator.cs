using FluentValidation;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Domain.PublicUsers.Repositories;

namespace Forum.Application.PublicUsers.Posts.Commands.Edit
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator(IPostDomainRepository postRepository)
            => this.Include(new PostCommandValidator<EditPostCommand>(postRepository));
    }
}
