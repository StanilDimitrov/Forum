using Forum.Domain.Common;
using System;
using System.Collections.Generic;

namespace Forum.Domain.PublicUsers.Models.Posts
{
    internal class CategoryData : IInitialData
    {
        public Type EntityType => typeof(Category);

        public IEnumerable<object> GetData()
            => new List<Category>
            {
                new Category("Movies", "Some movies description."),
                new Category("Books", "Some  books description."),
                new Category("Cinemas", "Some cinemas description.")
            };
    }
}
