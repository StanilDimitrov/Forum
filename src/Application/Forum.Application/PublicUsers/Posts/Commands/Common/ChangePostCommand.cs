using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Posts.Commands.Common
{
    internal static class ChangePostCommandExtensions
    {
        public static async Task<Result> UserHasPost(
            this ICurrentUser currentUser,
            IPublicUserRepository userRepository,
            int postId,
            CancellationToken cancellationToken)
        {
            var userId = await userRepository.GetPublicUserId(
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
