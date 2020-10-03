using Forum.Domain.Common;
using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Events.PublicUsers;
using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Posts;
using System.Collections.Generic;
using System.Linq;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Doman.PublicUsers.Models.Users
{
    public class PublicUser : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Post> posts;
        private readonly HashSet<Message> inboxMessages;

        internal PublicUser(string userName, string email, string userId)
        {
            this.ValidateUserName(userName);
            this.ValidateEmail(email);

            this.UserName = userName;
            this.Email = email;
            this.UserId = userId;
            this.posts = new HashSet<Post>();
            this.inboxMessages = new HashSet<Message>();
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public IReadOnlyCollection<Post> Posts => this.posts.ToList().AsReadOnly();

        public IReadOnlyCollection<Message> InboxMessages => this.inboxMessages.ToList().AsReadOnly();

        public IReadOnlyCollection<Message> SendMessages => this.inboxMessages.ToList().AsReadOnly();

        public string UserId { get; private set; }

        public void AddPost(Post post)
        {
            this.posts.Add(post);
        }

        public PublicUser UpdateEmail(string email)
        {
            this.ValidateEmail(email);
            this.Email = email;

            return this;
        }

        public Message SendMessage(string text, PublicUser reciever)
        {
            var message = new Message(text, reciever);
            reciever.RecieveMessage(message);
            return message;
        }

        public IReadOnlyList<Message> GetAllInboxMessages()
          => this.inboxMessages
              .OrderByDescending(x => x.CreatedOn)
              .ToList();

        public IReadOnlyList<Post> GetAllPosts()
        => this.posts
            .OrderByDescending(x => x.CreatedOn)
            .ToList();

        public void DeleteMessage(Message message)
        {
            this.inboxMessages.Remove(message);
        }

        private void RecieveMessage(Message message)
        {
            this.inboxMessages.Add(message);
            //Fire event to new bounded context for notifications
            //TODO Implement notifications bounded context
            this.RaiseEvent(new MessageReceivedEvent());
        }

        private void ValidateUserName(string userName)
        {
            Guard.ForStringLength<InvalidPublicUserException>(
                userName,
                MinNameLength,
                MaxNameLength,
                nameof(this.UserName));
        }

        private void ValidateEmail(string email)
        {
            Guard.ForStringLength<InvalidPublicUserException>(
                email,
                MinEmailLength,
                MaxEmailLength,
                nameof(this.Email));
        }
    }
}
