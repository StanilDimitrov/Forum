using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Doman.PublicUsers.Repositories;
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
            private readonly IPostDomainRepository postRepository;
            private readonly IPublicUserDomainRepository userRepository;

            public DeletePostCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository, 
                IPublicUserDomainRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                DeletePostCommand request, 
                CancellationToken cancellationToken)
            {
                var userHasPost = await this.currentUser.UserHasMessage(
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
