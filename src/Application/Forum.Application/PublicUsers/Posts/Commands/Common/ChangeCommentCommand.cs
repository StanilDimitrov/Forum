﻿using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Common
{
    internal static class ChangeCommentCommandExtensions
    {
        public static async Task<Result> UserHasComment(
            this ICurrentUser currentUser,
            IUserRepository userRepository,
            int commentId,
            CancellationToken cancellationToken)
        {
            var userId = await userRepository.GetUserId(
                currentUser.UserId,
                cancellationToken);

            var userHasComment = await userRepository.HasComment(
                userId,
                commentId,
                cancellationToken);

            return userHasComment
                ? Result.Success
                : "You cannot edit this comment.";
        }
    }
}
