using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Domain.PublicUsers.Factories.Users
{
    internal class PublicUserFactory : IPublicUserFactory
    {
        private string userName = default!;
        private string email = default!;
        private string publicUserUserId = default!;

        public IPublicUserFactory WithUserName(string userName)
        {
            this.userName = userName;
            return this;
        }

        public IPublicUserFactory WithEmail(string email)
        {
            this.email = email;
            return this;
        }

        public IPublicUserFactory FromUser(string userId)
        {
            this.publicUserUserId = userId;
            return this;
        }

        public PublicUser Build() => new PublicUser(this.userName, this.email, this.publicUserUserId);
    }
}
