using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Create.Comment
{
    public class CreateCommentCommand : CommentCommand<CreateCommentCommand>, IRequest<CreateCommentOutputModel>
    {
        public class CreateCarAdCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostRepository postRepository;

            public CreateCarAdCommandHandler(
                ICurrentUser currentUser,
                IPostRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }

            public async Task<CreateCommentOutputModel> Handle(
                CreateCommentCommand request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(
                    request.Id,
                    cancellationToken);

                post.AddComment(request.Description, request.ImageUrl, currentUser.UserId);

                await this.postRepository.Save(post, cancellationToken);

                return new CreateCommentOutputModel(post.Id);
            }
        }
    }
}
