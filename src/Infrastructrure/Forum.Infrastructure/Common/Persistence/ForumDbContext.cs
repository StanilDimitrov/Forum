using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Common.Events;
using Forum.Infrastructure.Identity;
using Forum.Infrastructure.PublicUsers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Common.Persistence
{
    internal class ForumDbContext : IdentityDbContext<User>,
        IPublicUsersDbContext
    {
        private readonly IEventDispatcher eventDispatcher;
        private bool eventsDispatched;

        public ForumDbContext(
            DbContextOptions<ForumDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;

            this.eventsDispatched = false;
        }

        public DbSet<Post> Posts { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<Like> Likes { get; set; } = default!;

        public DbSet<PublicUser> PublicUsers { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entriesModified = 0;

            if (!this.eventsDispatched)
            {
                var entities = this.ChangeTracker
                    .Entries<IEntity>()
                    .Select(e => e.Entity)
                    .Where(e => e.Events.Any())
                    .ToArray();

                foreach (var entity in entities)
                {
                    var events = entity.Events.ToArray();

                    entity.ClearEvents();

                    foreach (var domainEvent in events)
                    {
                        await this.eventDispatcher.Dispatch(domainEvent);
                    }
                }

                this.eventsDispatched = true;

                entriesModified = await base.SaveChangesAsync(cancellationToken);
            }

            return entriesModified;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
