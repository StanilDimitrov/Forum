using FluentValidation;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Posts.Commands.Edit;
using Forum.Doman.PublicUsers.Repositories;

namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Edit
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator(IPostDomainRepository postRepository)
            => this.Include(new PostCommandValidator<EditPostCommand>(postRepository));
    }
}
