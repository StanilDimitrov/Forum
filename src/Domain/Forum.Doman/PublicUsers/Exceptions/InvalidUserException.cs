using Forum.Domain.Exceptions;

namespace Forum.Doman.PublicUsers.Exceptions
{
    public class InvalidUserException : BaseDomainException
    {
        public InvalidUserException()
        {
        }

        public InvalidUserException(string error) => this.Error = error;
    }
}
