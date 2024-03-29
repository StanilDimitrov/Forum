﻿using Forum.Application.Common;
using Forum.Application.PublicUsers.Posts.Commands.Create;
using Forum.Application.PublicUsers.Posts.Commands.Delete;
using Forum.Application.PublicUsers.Posts.Commands.Edit;
using Forum.Application.PublicUsers.Posts.Queries.Categories;
using Forum.Application.PublicUsers.Posts.Queries.Comments;
using Forum.Application.PublicUsers.Posts.Queries.Details;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class PostsController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PostDetailsOutputModel>> Details(
            [FromRoute] PostDetailsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<IEnumerable<GetPostCommentOutputModel>>> GetPostComments(
             [FromRoute] GetPostCommentsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<IEnumerable<GetPostCategoryOutputModel>>> GetPostCategories(
            [FromRoute] GetPostCategoriesQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreatePostOutputModel>> Create(
            CreatePostCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id,
            EditPostCommand command)
            => await this.Send(command.SetId(id));

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeletePostCommand command)
            => await this.Send(command);
    }
}
