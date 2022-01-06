using Forum.Domain.Common;

namespace Forum.Domain.PublicUsers.Exceptions
{
    public class InvalidPostException : BaseDomainException
    {
        public InvalidPostException()
        {
        }

        public InvalidPostException(string error) => this.Error = error;
    }
}
