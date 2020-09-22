using Forum.Domain.Common;
using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Events;
using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Posts;
using System.Collections.Generic;
using System.Linq;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Doman.PublicUsers.Models.Users
{
    public class User : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Post> posts;
        private readonly HashSet<Comment> comments;

        internal User(string userName, string email)
        {
            this.ValidateUserName(userName);
            this.ValidateEmail(email);

            this.UserName = userName;
            this.Email = email;
            this.comments = new HashSet<Comment>();
            this.posts = new HashSet<Post>();
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public IReadOnlyCollection<Comment> Comments => this.comments.ToList().AsReadOnly();

        public IReadOnlyCollection<Post> Posts => this.posts.ToList().AsReadOnly();

        public void AddPost(Post post)
        {
            this.posts.Add(post);

            this.AddEvent(new PostAddedEvent());
        }

        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);

            //this.AddEvent(new CarAdAddedEvent());
        }

        public User UpdateEmail(string email)
        {
            this.ValidateEmail(email);
            this.Email = email;

            return this;
        }

        private void ValidateUserName(string userName)
        {
            Guard.ForStringLength<InvalidUserException>(
                userName,
                MinNameLength,
                MaxNameLength,
                nameof(this.UserName));
        }

        private void ValidateEmail(string email)
        {
            Guard.ForStringLength<InvalidUserException>(
                email,
                MinEmailLength,
                MaxEmailLength,
                nameof(this.Email));
        }
    }
}
