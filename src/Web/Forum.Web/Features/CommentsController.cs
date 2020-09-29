using Forum.Application.PublicUsers.Comments.Commands.Create;
using Forum.Application.PublicUsers.Comments.Commands.Create.Comment;
using Forum.Application.PublicUsers.Comments.Commands.Delete;
using Forum.Application.PublicUsers.Posts.Commands.Edit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class CommentsController : ApiController
    {
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<ActionResult<CreateCommentOutputModel>> Create(
             [FromRoute] CreateCommentCommand command)
             => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(
            [FromRoute] EditCommentCommand command)
             => await this.Send(command);

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteCommentCommand command)
            => await this.Send(command);
    }
}
