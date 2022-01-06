using Forum.Domain.Common;

namespace Forum.Domain.PublicUsers.Exceptions
{
    public class InvalidPublicUserException : BaseDomainException
    {
        public InvalidPublicUserException()
        {
        }

        public InvalidPublicUserException(string error) => this.Error = error;
    }
}
