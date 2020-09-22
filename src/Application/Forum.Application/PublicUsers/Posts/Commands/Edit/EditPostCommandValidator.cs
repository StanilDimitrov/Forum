using FluentValidation;
using Forum.Application.PublicUsers.Posts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Posts.Commands.Edit;

namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Edit
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator(IPostRepository postRepository)
            => this.Include(new PostCommandValidator<EditPostCommand>(postRepository));
    }
}
