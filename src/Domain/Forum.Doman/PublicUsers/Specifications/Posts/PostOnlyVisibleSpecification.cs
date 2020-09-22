using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using System;
using System.Linq.Expressions;

namespace CarRentalSystem.Domain.Dealerships.Specifications.CarAds
{
    public class PostOnlyVisibleSpecification : Specification<Post>
    {
        private readonly bool onlyVisible;

        public PostOnlyVisibleSpecification(bool onlyAvailable) 
            => this.onlyVisible = onlyAvailable;

        public override Expression<Func<Post, bool>> ToExpression()
        {
            if (this.onlyVisible)
            {
                return post => post.IsVisible;
            }

            return post => true;
        }
    }
}
