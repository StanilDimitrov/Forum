using Forum.Domain.Exceptions;

namespace Forum.Doman.PublicUsers.Exceptions
{
    public class InvalidPostException : BaseDomainException
    {
        public InvalidPostException()
        {
        }

        public InvalidPostException(string error) => this.Error = error;
    }
}
