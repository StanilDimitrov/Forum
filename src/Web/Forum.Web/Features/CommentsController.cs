﻿using Forum.Application.Common;
using Forum.Application.PublicUsers.Comments.Commands.Create;
using Forum.Application.PublicUsers.Comments.Commands.Delete;
using Forum.Application.PublicUsers.Comments.Commands.Edit;
using Forum.Application.PublicUsers.Comments.Queries.Details;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class CommentsController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<CommentDetailsOutputModel>> Details(
           [FromRoute] CommentDetailsQuery query)
            => await this.Send(query);

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<ActionResult<CreateCommentOutputModel>> Create(int postId,
             CreateCommentCommand command)
             => await this.Send(command.SetId(postId));

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id,
             EditCommentCommand command)
             => await this.Send(command.SetId(id));

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteCommentCommand command)
            => await this.Send(command);
    }
}
