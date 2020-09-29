namespace Forum.Application.PublicUsers.Posts.Commands.Create
{
    public class CreatePostOutputModel
    {
        public CreatePostOutputModel(int postId) 
            => this.PostId = postId;

        public int PostId { get; }
    }
}
