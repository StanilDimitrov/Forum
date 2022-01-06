﻿using Forum.Domain.Common;
using Forum.Domain.PublicUsers.Models.Posts;
using Forum.Domain.PublicUsers.Models.Users;
using Forum.Infrastructure.Common.Events;
using Forum.Infrastructure.Identity;
using Forum.Infrastructure.PublicUsers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        private readonly Stack<object> savesChangesTracker;

        public ForumDbContext(
            DbContextOptions<ForumDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;
            this.savesChangesTracker = new Stack<object>();
        }

        public DbSet<Post> Posts { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<Like> Likes { get; set; } = default!;

        public DbSet<PublicUser> PublicUsers { get; set; } = default!;

        public DbSet<Message> Messages { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.savesChangesTracker.Push(new object());

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

            this.savesChangesTracker.Pop();

            if (!this.savesChangesTracker.Any())
            {
                return await base.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
