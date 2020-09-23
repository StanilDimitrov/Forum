using Forum.Domain.Common;
using Forum.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Forum.Infrastructure.Common.Persistence
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly ForumDbContext dbContext;
        private readonly IEnumerable<IInitialData> initialDataProviders;

        public DatabaseInitializer(
            ForumDbContext dbContext,
            IEnumerable<IInitialData> initialDataProviders)
        {
            this.dbContext = dbContext;
            this.initialDataProviders = initialDataProviders;
        }

        public void Initialize()
        {
            this.dbContext.Database.Migrate();

            foreach (var initialDataProvider in this.initialDataProviders)
            {
                if (this.DataSetIsEmpty(initialDataProvider.EntityType))
                {
                    var data = initialDataProvider.GetData();

                    foreach (var entity in data)
                    {
                        this.dbContext.Add(entity);
                    }
                }
            }

            this.dbContext.SaveChanges();
        }

        private bool DataSetIsEmpty(Type type)
        {
            var setMethod = this.GetType()
                .GetMethod(nameof(this.GetSet), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(type);

            var set = setMethod.Invoke(this, Array.Empty<object>());

            var countMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
                .MakeGenericMethod(type);

            var result = (int)countMethod.Invoke(null, new[] { set })!;

            return result == 0;
        }

        private DbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
            => this.dbContext.Set<TEntity>();
    }
}
