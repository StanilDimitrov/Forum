﻿using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Likes.Commands.Common;
using Forum.Application.PublicUsers.Posts;
using Forum.Doman.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Likes.Commands.Create.Comment
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
                    request.PostId,
                    cancellationToken);

                var isPostLikedByUser = await this.postRepository.CheckIsPostLikedByUser(
                    request.PostId, currentUser.UserId);

                if (!isPostLikedByUser)
                {
                    post.AddLike(request.IsLiked, currentUser.UserId);
                    await this.postRepository.Save(post, cancellationToken);
                }
               
                return Result.Success;
            }
        }
    }
}
