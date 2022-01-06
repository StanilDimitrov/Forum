using Forum.Domain.Common;
using Forum.Domain.PublicUsers.Models.Posts;
using System;
using System.Linq.Expressions;

namespace Forum.Domain.PublicUsers.Specifications.Posts
{
    public class PostByCategorySpecification : Specification<Post>
    {
        private readonly int? category;

        public PostByCategorySpecification(int? category) 
            => this.category = category;

        protected override bool Include => this.category != null;

        public override Expression<Func<Post, bool>> ToExpression()
            => post => post.Category.Id == this.category;
    }
}
