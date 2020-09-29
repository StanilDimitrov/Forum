﻿using Forum.Application.PublicUsers.Messages.Commands.Create;
using Forum.Application.PublicUsers.Messages.Commands.Delete;
using Forum.Application.PublicUsers.Messages.Commands.Read;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.Messages.Queries.Details;
using Forum.Application.PublicUsers.Users.Queries.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class MessagesController : ApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(
           CreateMessageCommand command)
           => await this.Send(command);

        [HttpPost]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<ReadNewMessageOutputModel>> ReadNewMessage(
           [FromRoute] ReadNewMessageCommand command)
           => await this.Send(command);

        [HttpGet]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<MessageOutputModel>> ReadOldMessage(
            [FromRoute] ReadOldMessageQuery query)
            => await this.Send(query);

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageOutputModel>>> GetSentMessages(
            [FromRoute] GetPublicUserSentMessagesQuery query)
            => await this.Send(query);

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageOutputModel>>> GetOldMessages(
             [FromRoute] GetPublicUserOldMessagesQuery query)
             => await this.Send(query);

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageOutputModel>>> GetNewMessages(
            [FromRoute] GetPublicUserNewMessagesQuery query)
            => await this.Send(query);

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> DeleteMessage(
            [FromRoute] DeleteMessageCommand command)
            => await this.Send(command);
    }
}
