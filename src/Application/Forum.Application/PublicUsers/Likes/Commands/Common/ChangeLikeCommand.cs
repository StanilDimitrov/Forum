using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Domain.PublicUsers.Models.Posts;

namespace Forum.Application.PublicUsers.Likes.Commands.Common
{
    internal static class ChangeLikeCommandExtensions
    {
        public static Result UserHasLike(
            this ICurrentUser currentUser,
            Like like)
        {
            var userId = currentUser.UserId;
            var userHasLike = UserHasLike(userId, like);

            return userHasLike
                ? Result.Success
                : "You cannot edit this like.";
        }

        private static bool UserHasLike(string userId, Like like)
        {
            return like.UserId == userId;
        }
    }
}
