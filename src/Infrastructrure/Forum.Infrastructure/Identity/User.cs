using Forum.Application.Identity;
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
    }
}
