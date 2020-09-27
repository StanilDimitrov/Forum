using Forum.Application.Common;
using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Posts;
using Forum.Doman.PublicUsers.Models.Posts;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.Comments.Commands.Common
{
    internal static class ChangeCommentCommandExtensions
    {
        public static async Task<Result> UserHasComment(
            this ICurrentUser currentUser,
            IPostRepository postRepository,
            int commentId,
            CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;

            var comment = await postRepository.GetComment(
                commentId,
                cancellationToken);

            var userHasComment = UserHasComment(userId, comment);

            return userHasComment
                ? Result.Success
                : "You cannot edit this comment.";
        }

        private static bool UserHasComment(string userId, Comment comment)
        {
            return comment.UserId == userId;
        }
    }
}
