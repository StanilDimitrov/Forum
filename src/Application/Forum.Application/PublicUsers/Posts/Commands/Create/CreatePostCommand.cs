using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Domain.PublicUsers.Factories.Posts;
using Forum.Doman.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Create
{
    public class CreatePostCommand : PostCommand<CreatePostCommand>, IRequest<CreatePostOutputModel>
    {
        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserDomainRepository userRepository;
            private readonly IPostDomainRepository postRepository;
            private readonly IPostFactory postFactory;

            public CreatePostCommandHandler(
                ICurrentUser currentUser, 
                IPublicUserDomainRepository userRepository,
                IPostDomainRepository postRepository,
                IPostFactory postFactory)
            {
                this.currentUser = currentUser;
                this.userRepository = userRepository;
                this.postRepository = postRepository;
                this.postFactory = postFactory;
            }

            public async Task<CreatePostOutputModel> Handle(
                CreatePostCommand request, 
                CancellationToken cancellationToken)
            {
                var user = await this.userRepository.FindByCurrentUser(
                    this.currentUser.UserId, 
                    cancellationToken);

                var category = await this.postRepository.GetCategory(
                    request.Category, 
                    cancellationToken);

                var post = this.postFactory
                    .WithDescription(request.Description)
                    .WithCategory(category)
                    .WithImageUrl(request.ImageUrl)
                    .Build();

                user.AddPost(post);

                await this.postRepository.Save(post, cancellationToken);

                return new CreatePostOutputModel(post.Id);
            }
        }
    }
}
