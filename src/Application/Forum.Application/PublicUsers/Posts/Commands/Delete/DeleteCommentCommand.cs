using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Delete
{
    public class DeleteCommentCommand : CommentCommand<DeleteCommentCommand>, IRequest<Result>
    {
        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostRepository postRepository;
            private readonly IPublicUserRepository userRepository;

            public DeleteCommentCommandHandler(
                ICurrentUser currentUser,
                IPostRepository postRepository,
                IPublicUserRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                DeleteCommentCommand request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.PostId, cancellationToken);
                var userHasComment = await this.currentUser.UserHasComment(
                    this.userRepository,
                    request.Id,
                    cancellationToken);

                if (!userHasComment)
                {
                    return userHasComment;
                }

                post.DeleteComment(request.Id);

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
