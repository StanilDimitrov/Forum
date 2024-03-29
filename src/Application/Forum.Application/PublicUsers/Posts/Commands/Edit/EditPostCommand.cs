﻿using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts.Commands.Common;
using Forum.Domain.PublicUsers.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Edit
{
    public class EditPostCommand : PostCommand<EditPostCommand>, IRequest<Result>
    {
        public class EditPostCommandHandler : IRequestHandler<EditPostCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostDomainRepository postRepository;
            private readonly IPublicUserDomainRepository userRepository;

            public EditPostCommandHandler(
                ICurrentUser currentUser,
                IPostDomainRepository postRepository,
                IPublicUserDomainRepository userRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(
                EditPostCommand request, 
                CancellationToken cancellationToken)
            {
                var userHasPost = await this.currentUser.UserHasMessage(
                    this.userRepository,
                    request.Id,
                    cancellationToken);

                if (!userHasPost)
                {
                    return userHasPost;
                }

                var category = await this.postRepository.GetCategory(
                    request.Category, 
                    cancellationToken);

                var post = await this.postRepository
                    .Find(request.Id, cancellationToken);

                post.UpdateDescription(request.Description)
                    .UpdateCategory(category);

                await this.postRepository.Save(post, cancellationToken);

                return Result.Success;
            }
        }
    }
}
