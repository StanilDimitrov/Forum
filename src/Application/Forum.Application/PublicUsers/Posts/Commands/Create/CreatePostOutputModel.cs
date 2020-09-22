namespace Forum.Application.PublicUsers.CarAds.Commands.Create
{
    public class CreatePostOutputModel
    {
        public CreatePostOutputModel(int postId) 
            => this.PostId = postId;

        public int PostId { get; }
    }
}
