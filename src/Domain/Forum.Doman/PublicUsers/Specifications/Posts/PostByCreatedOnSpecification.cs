using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using System;
using System.Linq.Expressions;

namespace Forum.Domain.PublicUsers.Specifications.Posts
{
    public class PostByCreatedOnSpecification : Specification<Post>
    {
        private readonly DateTime startDate;
        private readonly DateTime endDate;

        public PostByCreatedOnSpecification(
            DateTime? startDate = default, 
            DateTime? endDate = default)
        {
            this.startDate = startDate ?? default;
            this.endDate = endDate ?? DateTime.Now;
        }

        public override Expression<Func<Post, bool>> ToExpression()
            => post => this.startDate < post.CreatedOn && post.CreatedOn < this.endDate;
    }
}
