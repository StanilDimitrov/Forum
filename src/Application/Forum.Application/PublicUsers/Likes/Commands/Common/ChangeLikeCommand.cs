using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts;
using Forum.Doman.PublicUsers.Models.Posts;
using System.Threading;
using System.Threading.Tasks;

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
