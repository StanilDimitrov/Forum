using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Domain.PublicUsers.Factories.Users
{
    internal class UserFactory : IUserFactory
    {
        private string userName = default!;
        private string email = default!;

        public IUserFactory WithUserName(string userName)
        {
            this.userName = userName;
            return this;
        }

        public IUserFactory WithEmail(string email)
        {
            this.email = email;
            return this;
        }

        public User Build() => new User(this.userName, this.email);

        public User Build(string userName, string email)
            => this
                .WithUserName(userName)
                .WithEmail(email)
                .Build();
    }
}
