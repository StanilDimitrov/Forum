using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Exceptions;
using System;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;

namespace Forum.Doman.PublicUsers.Models.Posts
{
    public class Comment : Entity<int>
    {
        internal Comment(string description, string userId)
        {
            this.ValidateDescription(description);

            this.Description = description;
            this.UserId = userId;
            this.CreatedOn = DateTime.Now;
        }

        public string? UserId { get; private set; }

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
