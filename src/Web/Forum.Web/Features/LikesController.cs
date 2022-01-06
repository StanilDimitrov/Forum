using Forum.Application.Common;
using Forum.Application.PublicUsers.Likes.Commands.Create;
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
        public async Task<ActionResult> Create(int postId,
             CreatePostLikeCommand command)
            => await this.Send(command.SetId(postId));

        [HttpPut("{postId}")]
        [Authorize]
        public async Task<ActionResult> Edit(int postId,
            EditPostLikeCommand command)
            => await this.Send(command.SetId(postId));
    }
}
