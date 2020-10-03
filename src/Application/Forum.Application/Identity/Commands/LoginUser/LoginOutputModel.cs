namespace Forum.Application.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token, int publicUserId)
        {
            this.Token = token;
        }

        public int PublicUserId { get; }

        public string Token { get; }
    }
}
