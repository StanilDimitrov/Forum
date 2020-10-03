using FluentValidation;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Doman.PublicUsers.Repositories;

namespace Forum.Application.PublicUsers.Posts.Commands.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator(IPostDomainRepository postRepository) 
            => this.Include(new PostCommandValidator<CreatePostCommand>(postRepository));
    }
}
