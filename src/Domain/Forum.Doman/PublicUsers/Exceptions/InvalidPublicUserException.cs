using Forum.Domain.Exceptions;

namespace Forum.Doman.PublicUsers.Exceptions
{
    public class InvalidPublicUserException : BaseDomainException
    {
        public InvalidPublicUserException()
        {
        }

        public InvalidPublicUserException(string error) => this.Error = error;
    }
}
