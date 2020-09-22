using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Application.PublicUsers.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Edit
{
    public class EditCommentCommand : CommentCommand<EditCommentCommand>, IRequest<Result>
    {
        public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostRepository postRepository;
            private readonly IUserRepository userRepository;

            public EditCommentCommandHandler(
                ICurrentUser currentUser,
                IPostRepository postRepository,
                IUserRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                EditCommentCommand request,
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

                var post = await this.postRepository
                    .Find(request.PostId, cancellationToken);

                post.UpdateComment(
                    request.Id,
                    request.Description,
                    request.ImageUrl);

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
