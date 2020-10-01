using Forum.Domain.Common;
using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Events;
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
        private readonly HashSet<Message> sentMessages;
        private readonly HashSet<Message> unreadMessages;
        private readonly HashSet<Message> readMessages;

        internal PublicUser(string userName, string email)
        {
            this.ValidateUserName(userName);
            this.ValidateEmail(email);

            this.UserName = userName;
            this.Email = email;
            this.posts = new HashSet<Post>();
            this.sentMessages = new HashSet<Message>();
            this.unreadMessages = new HashSet<Message>();
            this.readMessages = new HashSet<Message>();
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public IReadOnlyCollection<Post> Posts => this.posts.ToList().AsReadOnly();

        public IReadOnlyCollection<Message> SentMessages => this.sentMessages.ToList().AsReadOnly();

        public IReadOnlyCollection<Message> UnreadMessages => this.unreadMessages.ToList().AsReadOnly();

        public IReadOnlyCollection<Message> ReadMessages => this.readMessages.ToList().AsReadOnly();

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
            this.sentMessages.Add(message);
            reciever.RecieveMessage(message);
            return message;
        }

        public void ReadNewMessage(Message message)
        {
            this.readMessages.Add(message);
            this.unreadMessages.Remove(message);
        }

        public IReadOnlyList<Message> GetAllUnReadMessages()
          => this.unreadMessages
              .OrderByDescending(x => x.CreatedOn)
              .ToList();

        public IReadOnlyList<Message> GetAllReadMessages()
         => this.readMessages
             .OrderByDescending(x => x.CreatedOn)
             .ToList();

        public IReadOnlyList<Post> GetAllPosts()
        => this.posts
            .OrderByDescending(x => x.CreatedOn)
            .ToList();

        public IReadOnlyList<Message> GetAllSentMessages()
       => this.sentMessages
           .OrderByDescending(x => x.CreatedOn)
           .ToList();

        public void DeleteMessage(Message message)
        {
            this.readMessages.Remove(message);
        }

        private void RecieveMessage(Message message)
        {
            this.unreadMessages.Add(message);
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
