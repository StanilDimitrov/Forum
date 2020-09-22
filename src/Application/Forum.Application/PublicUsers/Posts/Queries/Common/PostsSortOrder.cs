using Forum.Application.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using System;
using System.Linq.Expressions;

namespace Forum.Application.PublicUsers.Posts.Queries.Common
{
    public class PostsSortOrder : SortOrder<Post>
    {
        public PostsSortOrder(string? sortBy, string? order)
            : base(sortBy, order)
        {
        }

        public override Expression<Func<Post, object>> ToExpression()
            => this.SortBy switch
            {
                "createdOn" => post => post.CreatedOn,
                _ => post => post.Id
            };
    }
}
