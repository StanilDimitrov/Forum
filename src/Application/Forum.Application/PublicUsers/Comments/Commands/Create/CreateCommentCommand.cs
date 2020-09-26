using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Commands.Common;
using Forum.Application.PublicUsers.Posts;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Comments.Commands.Create.Comment
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
                    request.PostId,
                    cancellationToken);

                post.AddComment(request.Description, request.ImageUrl, currentUser.UserId);

                await this.postRepository.Save(post, cancellationToken);
                return new CreateCommentOutputModel(post.Comments.Last().Id);
            }
        }
    }
}
