using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Common
{
    internal static class ChangeCarAdCommandExtensions
    {
        public static async Task<Result> UserHasPost(
            this ICurrentUser currentUser,
            IUserRepository userRepository,
            int postId,
            CancellationToken cancellationToken)
        {
            var userId = await userRepository.GetUserId(
                currentUser.UserId,
                cancellationToken);

            var userHasPost = await userRepository.HasPost(
                userId,
                postId,
                cancellationToken);

            return userHasPost
                ? Result.Success
                : "You cannot edit this post.";
        }
    }
}
