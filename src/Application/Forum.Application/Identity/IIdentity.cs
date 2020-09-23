using Forum.Application.Common;
using Forum.Application.Identity.Commands;
using Forum.Application.Identity.Commands.ChangePassword;
using Forum.Application.Identity.Commands.LoginUser;
using System.Threading.Tasks;

namespace Forum.Application.Identity
{
    public interface IIdentity
    {
        Task<Result<IUser>> Register(UserInputModel userInput);

        Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput);

    }
}
