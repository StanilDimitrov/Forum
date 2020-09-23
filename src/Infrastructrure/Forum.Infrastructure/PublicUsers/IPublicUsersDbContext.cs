using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Common.Persistence;
using Forum.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.PublicUsers
{
    public interface IPublicUsersDbContext : IDbContext
    {
        DbSet<Post> Posts { get;  } 

        DbSet<Category> Categories { get; } 

        DbSet<Comment> Comments { get;  } 

        DbSet<PublicUser> PublicUsers { get; }

        DbSet<User> Users { get; } 
    }
}
