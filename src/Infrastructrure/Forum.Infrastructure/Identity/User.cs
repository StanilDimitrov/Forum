using Forum.Application.Identity;
using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Forum.Infrastructure.Identity
{
    public class User : IdentityUser, IUser
    {
        internal User(string email)
            : base(email)
        {
            this.Email = email;
        }

        public PublicUser? PublicUser { get; private set; }


        public void BecomePublicUser(PublicUser publicUser)
        {
            if (this.PublicUser != null)
            {
                throw new InvalidPublicUserException($"User '{this.UserName}' is already a public user.");
            }

            this.PublicUser = publicUser;
        }
    }
}
