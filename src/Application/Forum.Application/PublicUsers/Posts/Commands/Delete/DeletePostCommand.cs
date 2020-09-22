using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Delete
{
    public class DeletePostCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostRepository postRepository;
            private readonly IUserRepository userRepository;

            public DeletePostCommandHandler(
                ICurrentUser currentUser, 
                IPostRepository postRepository, 
                IUserRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                DeletePostCommand request, 
                CancellationToken cancellationToken)
            {
                var userHasPost = await this.currentUser.UserHasPost(
                    this.userRepository,
                    request.Id,
                    cancellationToken);

                if (!userHasPost)
                {
                    return userHasPost;
                }

                return await this.postRepository.Delete(
                    request.Id, 
                    cancellationToken);
            }
        }
    }
}
