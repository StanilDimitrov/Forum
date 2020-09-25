using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Exceptions;
using System;
using System.Runtime.CompilerServices;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;

namespace Forum.Doman.PublicUsers.Models.Posts
{
    public class Comment : Entity<int>
    {
        internal Comment(string description, string imageUrl, string userId)
        {
            this.ValidateDescription(description);
            this.ValidateImageUrl(imageUrl);

            this.Description = description;
            this.ImageUrl = imageUrl;
            this.UserId = userId;
            this.CreatedOn = DateTime.Now;
        }

        public string? UserId { get; private set; }

        public string Description { get; private set; }

        public string ImageUrl { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public Comment UpdateDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        public Comment UpdateImageUrl(string imageUrl)
        {
            this.ValidateDescription(imageUrl);
            this.ImageUrl = imageUrl;

            return this;
        }

        public void ValidateDescription(string description)
          => Guard.ForStringLength<InvalidPostException>(
              description,
              MinDescriptionLength,
              MaxDescriptionLength,
              nameof(this.Description));

        private void ValidateImageUrl(string imageUrl)
            => Guard.ForValidUrl<InvalidPostException>(
                imageUrl,
                nameof(this.ImageUrl));
    }
}
