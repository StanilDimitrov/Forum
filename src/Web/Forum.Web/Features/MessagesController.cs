using Forum.Application.Common;
using Forum.Application.PublicUsers.Messages.Commands.Create;
using Forum.Application.PublicUsers.Messages.Commands.Delete;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class MessagesController : ApiController
    {
        [HttpPost("{receiverId}")]
        [Authorize]
        public async Task<ActionResult> Create(int receiverId,
           CreateMessageCommand command)
           => await this.Send(command.SetId(receiverId));

        [HttpGet]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<IEnumerable<MessageOutputModel>>> GetInboxMessages(
           [FromRoute] GetPublicUserInboxMessagesQuery query)
             => await this.Send(query);

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteMessageCommand command)
            => await this.Send(command);
    }
}
