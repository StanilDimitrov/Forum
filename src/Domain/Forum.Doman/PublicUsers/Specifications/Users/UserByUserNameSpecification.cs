using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Users;
using System;
using System.Linq.Expressions;

namespace Forum.Domain.PublicUsers.Specifications.Users
{
    public class UserByUserNameSpecification : Specification<User>
    {
        private readonly string? userName;

        public UserByUserNameSpecification(string? userName) 
            => this.userName = userName;

        protected override bool Include => this.userName != null;

        public override Expression<Func<User, bool>> ToExpression()
            => user => user.UserName.ToLower().Contains(this.userName!.ToLower());
    }
}
