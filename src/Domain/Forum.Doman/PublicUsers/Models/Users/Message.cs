using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Exceptions;
using System;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Message;

namespace Forum.Doman.PublicUsers.Models.Users
{
    public class Message : Entity<int>
    {
        internal Message(string text, PublicUser reciever)
        {
            this.ValidateText(text);

            this.Text = text;
            this.Reciever = reciever;
            this.CreatedOn = DateTime.Now;
        }

        public PublicUser Reciever { get; private set; }

        public string Text { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public Message UpdateText(string description)
        {
            this.ValidateText(description);
            this.Text = description;

            return this;
        }

        public void ValidateText(string text)
         => Guard.ForStringLength<InvalidPublicUserException>(
             text,
             MinTextLenght,
             MaxTextLength,
             nameof(this.Text));
    }
}
