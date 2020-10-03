namespace Forum.Application.PublicUsers.PublicUsers.Commands.Create
{
    public class CreatePublicUserOutputModel
    {
        public CreatePublicUserOutputModel(int publicUserId)
           => this.PublicUserId = publicUserId;

        public int PublicUserId { get; }
    }
}
