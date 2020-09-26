namespace Forum.Application.PublicUsers.Comments.Commands.Create
{
    public class CreateCommentOutputModel
    {
        public CreateCommentOutputModel(int commentId)
            => this.CommentId = commentId;

        public int CommentId { get; }
    }
}
