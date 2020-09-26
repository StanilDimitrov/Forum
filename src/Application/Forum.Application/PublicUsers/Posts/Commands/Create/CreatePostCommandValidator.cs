using Forum.Application.Dealerships.CarAds.Commands.Create;
using FluentValidation;
using Forum.Application.PublicUsers.Posts.Commands.Common;

namespace Forum.Application.PublicUsers.Posts.Commands.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator(IPostRepository postRepository) 
            => this.Include(new PostCommandValidator<CreatePostCommand>(postRepository));
    }
}
