using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Users;
using System;
using System.Linq.Expressions;

namespace Forum.Domain.PublicUsers.Specifications.Users
{
    public class UserByIdSpecification : Specification<PublicUser>
    {
        private readonly int? id;

        public UserByIdSpecification(int? id)
            => this.id = id;

        protected override bool Include => this.id != null;

        public override Expression<Func<PublicUser, bool>> ToExpression()
            => post => post.Id == this.id;
    }
}
