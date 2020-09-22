using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Delete
{
    public class DeleteCommentCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostRepository postRepository;
            private readonly IUserRepository userRepository;

            public DeleteCommentCommandHandler(
                ICurrentUser currentUser,
                IPostRepository postRepository,
                IUserRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                DeleteCommentCommand request,
                CancellationToken cancellationToken)
            {
                var userHasComment = await this.currentUser.UserHasComment(
                    this.userRepository,
                    request.Id,
                    cancellationToken);

                if (!userHasComment)
                {
                    return userHasComment;
                }

                return await this.postRepository.Delete(
                    request.Id,
                    cancellationToken);
            }
        }
    }
}
