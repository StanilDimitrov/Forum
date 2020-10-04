using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Commands.Common;
using Forum.Doman.PublicUsers.Repositories;
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
            private readonly IPostDomainRepository postRepository;

            public EditCommentCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }

            public async Task<Result> Handle(
                EditCommentCommand request,
                CancellationToken cancellationToken)
            {
                var userHasComment = await this.currentUser.UserHasComment(
                    postRepository,
                    request.Id,
                    cancellationToken);

                if (!userHasComment)
                {
                    return userHasComment;
                }

                var post = await this.postRepository
                    .GetPostByCommentId(request.Id, cancellationToken);

                var comment = await this.postRepository
                    .GetComment(request.Id, cancellationToken);

                post.UpdateComment(
                    comment,
                    request.Description);

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
