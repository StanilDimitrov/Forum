using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Commands.Common;
using Forum.Doman.PublicUsers.Repositories;
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
            private readonly IPostDomainRepository postRepository;
            private readonly IPublicUserDomainRepository userRepository;

            public DeleteCommentCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository,
                IPublicUserDomainRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                DeleteCommentCommand request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.GetPostByCommentId(request.Id, cancellationToken);
                var publicUser = await userRepository.FindByCurrentUser(currentUser.UserId);
                var comment = await this.postRepository.GetComment(request.Id);

                if (comment.PublicUser != publicUser)
                {
                    return false;
                }

                post.DeleteComment(comment);

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
