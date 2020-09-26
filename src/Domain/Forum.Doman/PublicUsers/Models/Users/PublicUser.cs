﻿using Forum.Domain.Common;
using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Events;
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

        internal PublicUser(string userName, string email)
        {
            this.ValidateUserName(userName);
            this.ValidateEmail(email);

            this.UserName = userName;
            this.Email = email;
            this.posts = new HashSet<Post>();
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public IReadOnlyCollection<Post> Posts => this.posts.ToList().AsReadOnly();

        public void AddPost(Post post)
        {
            this.posts.Add(post);

            this.AddEvent(new PostAddedEvent());
        }

        public PublicUser UpdateEmail(string email)
        {
            this.ValidateEmail(email);
            this.Email = email;

            return this;
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