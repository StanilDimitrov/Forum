using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Commands.Common;
using Forum.Application.PublicUsers.Posts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Comments.Commands.Delete
{
    public class DeleteCommentCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostRepository postRepository;

            public DeleteCommentCommandHandler(
                ICurrentUser currentUser,
                IPostRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }

            public async Task<Result> Handle(
                DeleteCommentCommand request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.GetPostByCommentId(request.Id, cancellationToken);
                var userHasComment = await this.currentUser.UserHasComment(
                    this.postRepository,
                    request.Id,
                    cancellationToken);

                if (!userHasComment)
                {
                    return userHasComment;
                }

                var comment = await this.postRepository.GetComment(request.Id);
                post.DeleteComment(comment);

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
