using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.PublicUsers
{
    internal interface IPublicUsersDbContext : IDbContext
    {
        DbSet<Post> Posts { get;  } 

        DbSet<Category> Categories { get; } 

        DbSet<Comment> Comments { get;  }

        DbSet<Like> Likes { get; }

        DbSet<PublicUser> PublicUsers { get; }

        DbSet<Message> Messages { get; }
    }
}
