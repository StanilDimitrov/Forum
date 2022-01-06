using Forum.Domain.Common.Models;
using Forum.Domain.PublicUsers.Exceptions;
using Forum.Domain.PublicUsers.Models.Users;
using System;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;

namespace Forum.Domain.PublicUsers.Models.Posts
{
    public class Comment : Entity<int>
    {
        internal Comment(string description, PublicUser publicUser)
        {
            this.ValidateDescription(description);

            this.Description = description;
            this.PublicUser = publicUser;
            this.CreatedOn = DateTime.Now;
        }

        private Comment(string description)
        {
            this.ValidateDescription(description);

            this.Description = description;
            this.PublicUser = default!;
            this.CreatedOn = DateTime.Now;
        }

        public PublicUser PublicUser { get; private set; }

        public string Description { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public Comment UpdateDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        public void ValidateDescription(string description)
          => Guard.ForStringLength<InvalidPostException>(
              description,
              MinDescriptionLength,
              MaxDescriptionLength,
              nameof(this.Description));
    }
}
