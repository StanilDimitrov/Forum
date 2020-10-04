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
            private readonly IPublicUserDomainRepository userRepository;

            public EditCommentCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository,
                IPublicUserDomainRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                EditCommentCommand request,
                CancellationToken cancellationToken)
            {

                var post = await this.postRepository
                    .GetPostByCommentId(request.Id, cancellationToken);
                var comment = await this.postRepository
                    .GetComment(request.Id, cancellationToken);
                var publicUser = await this.userRepository.FindByCurrentUser(currentUser.UserId);

                if (comment.PublicUser != publicUser)
                {
                    return false;
                }

                post.UpdateComment(
                    comment,
                    request.Description);

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
