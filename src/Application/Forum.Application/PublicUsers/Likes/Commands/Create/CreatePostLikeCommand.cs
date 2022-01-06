using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Likes.Commands.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Forum.Domain.PublicUsers.Repositories;

namespace Forum.Application.PublicUsers.Likes.Commands.Create
{
    public class CreatePostLikeCommand : PostLikeCommand<CreatePostLikeCommand>, IRequest<Result>
    {
        public class CreatePostLikeCommandHandler : IRequestHandler<CreatePostLikeCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostDomainRepository postRepository;

            public CreatePostLikeCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }

            public async Task<Result> Handle(
                CreatePostLikeCommand request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(
                    request.Id,
                    cancellationToken);

                var isPostLikedByUser = await this.postRepository.CheckIsPostLikedByUser(
                    request.Id, currentUser.UserId);

                if (!isPostLikedByUser)
                {
                    post.AddLike(request.IsLike, currentUser.UserId);
                    await this.postRepository.Save(post, cancellationToken);
                }
               
                return Result.Success;
            }
        }
    }
}
