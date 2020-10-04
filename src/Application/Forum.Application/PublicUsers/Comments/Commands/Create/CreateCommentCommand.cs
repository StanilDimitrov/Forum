using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Comments.Commands.Common;
using Forum.Application.PublicUsers.Posts;
using Forum.Doman.PublicUsers.Repositories;
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
            private readonly IPostDomainRepository postRepository;
            private readonly IPublicUserDomainRepository publicUserRepository;

            public CreateCarAdCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository,
                IPublicUserDomainRepository publicUserRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.publicUserRepository = publicUserRepository;
            }

            public async Task<CreateCommentOutputModel> Handle(
                CreateCommentCommand request,
                CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(
                    request.Id,
                    cancellationToken);

                var publicUser = await this.publicUserRepository.FindByCurrentUser(currentUser.UserId);

                post.AddComment(request.Description, publicUser);

                await this.postRepository.Save(post, cancellationToken);
                return new CreateCommentOutputModel(post.Comments.Last().Id);
            }
        }
    }
}
