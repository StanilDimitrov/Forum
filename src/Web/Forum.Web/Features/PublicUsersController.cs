using CarRentalSystem.Application.Dealerships.Dealers.Queries.Details;
using Forum.Application.Common;
using Forum.Application.PublicUsers.Users.Commands.Edit;
using Forum.Application.PublicUsers.Users.Queries.Details;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class PublicUsersController : ApiController
    {
        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PublicUserDetailsOutputModel>> Details(
            [FromRoute] PublicUserDetailsQuery query)
            => await this.Send(query);

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Edit(
            int id, EditPublicUserCommand command)
            => await this.Send(command.SetId(id));
    }
}
