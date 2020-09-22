using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Posts;
using System;
using System.Linq.Expressions;

namespace CarRentalSystem.Domain.Dealerships.Specifications.CarAds
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
