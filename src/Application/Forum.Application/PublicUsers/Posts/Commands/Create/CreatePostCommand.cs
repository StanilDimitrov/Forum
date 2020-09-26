using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.CarAds.Commands.Create;
using Forum.Application.PublicUsers.Posts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Users;
using Forum.Domain.PublicUsers.Factories.Posts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.Dealerships.CarAds.Commands.Create
{
    public class CreatePostCommand : PostCommand<CreatePostCommand>, IRequest<CreatePostOutputModel>
    {
        public class CreateCarAdCommandHandler : IRequestHandler<CreatePostCommand, CreatePostOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserRepository userRepository;
            private readonly IPostRepository postRepository;
            private readonly IPostFactory postFactory;

            public CreateCarAdCommandHandler(
                ICurrentUser currentUser, 
                IPublicUserRepository userRepository,
                IPostRepository postRepository,
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
