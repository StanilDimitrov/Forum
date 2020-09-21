using Forum.Doman.PublicUsers.Models.Posts;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Doman.PublicUsers.Models.Users
{
    public class User
    {
        private readonly HashSet<Post> posts;
        private readonly HashSet<Comment> comments;
        internal User(string userName, string email)
        {
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
            this.UserName = userName;
            this.Email = email;
        }
        public string UserName { get; private set; }

        public string Email { get; private set; }

        public IReadOnlyCollection<Comment> Comments => this.comments.ToList().AsReadOnly();

        public IReadOnlyCollection<Post> Posts => this.posts.ToList().AsReadOnly();

        public void AddPost(Post post)
        {
            this.posts.Add(post);

            //this.AddEvent(new CarAdAddedEvent());
        }

        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);

            //this.AddEvent(new CarAdAddedEvent());
        }

        public User UpdateEmail(string email)
        {
            this.Email = email;

            return this;
        }
    }
}
