using Forum.Application.Common;
using Forum.Application.PublicUsers.PublicUsers.Commands.Create;
using Forum.Application.PublicUsers.PublicUsers.Commands.Edit;
using Forum.Application.PublicUsers.PublicUsers.Queries.Details;
using Forum.Application.PublicUsers.PublicUsers.Queries.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class PublicUsersController : ApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreatePublicUserOutputModel>> Create(
            CreatePublicUserCommand command)
            => await this.Send(command);

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Edit(
           int id, EditPublicUserCommand command)
           => await this.Send(command.SetId(id));

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PublicUserDetailsOutputModel>> Details(
            [FromRoute] PublicUserDetailsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<IEnumerable<GetPublicUserPostOutputModel>>> GetPublicUserPosts(
            [FromRoute] GetPublicUserPostsQuery query)
            => await this.Send(query);
    }
}
