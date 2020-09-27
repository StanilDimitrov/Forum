using Forum.Application.Identity.Commands.ChangePassword;
using Forum.Application.Identity.Commands.CreateUser;
using Forum.Application.Identity.Commands.LoginUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Features
{
    public class IdentityController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Register(
            CreateUserCommand command)
            => await this.Send(command);

        [HttpPost]
        public async Task<ActionResult<LoginOutputModel>> Login(
            LoginUserCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> ChangePassword(
            ChangePasswordCommand command)
            => await this.Send(command);
    }
}
