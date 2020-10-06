using Forum.Doman.Common;
using Forum.Doman.PublicUsers.Models.Users;
using System;
using System.Linq.Expressions;

namespace Forum.Domain.PublicUsers.Specifications.Users
{
    public class PublicUserByUserNameSpecification : Specification<PublicUser>
    {
        private readonly string? userName;

        public PublicUserByUserNameSpecification(string? userName) 
            => this.userName = userName;

        protected override bool Include => this.userName != null;

        public override Expression<Func<PublicUser, bool>> ToExpression()
            => user => user.UserName.ToLower().Contains(this.userName!.ToLower());
    }
}
