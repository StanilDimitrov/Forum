using Forum.Application.PublicUsers.Likes.Commands.Create.Comment;
using Forum.Application.PublicUsers.Likes.Commands.Edit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class LikesController : ApiController
    {
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<ActionResult> Create(
            [FromRoute] CreatePostLikeCommand command)
            => await this.Send(command);

        [HttpPut("{postId}")]
        [Authorize]
        public async Task<ActionResult> Edit(
            [FromRoute] EditPostLikeCommand command)
            => await this.Send(command);
    }
}
