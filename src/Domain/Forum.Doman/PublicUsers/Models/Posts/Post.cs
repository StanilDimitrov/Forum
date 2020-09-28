using Forum.Domain.Common;
using Forum.Domain.Common.Models;
using Forum.Domain.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Post;

namespace Forum.Doman.PublicUsers.Models.Posts
{
    public class Post : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Comment> comments;
        private readonly HashSet<Like> likes;

        private static readonly IEnumerable<Category> AllowedCategories
         = new CategoryData().GetData().Cast<Category>();

        internal Post(
            string description,
            string imageUrl,
            Category category)

        {
            this.ValidateImageUrl(imageUrl);
            this.ValidateDescription(description);
            this.ValidateCategory(category);

            this.Description = description;
            this.ImageUrl = imageUrl;
            this.Category = category;
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
        }

        private Post(
           string description,
           string imageUrl)

        {
            this.Description = description;
            this.CreatedOn = DateTime.Now;
            this.ImageUrl = imageUrl;
            this.Category = default!;
            this.comments = new HashSet<Comment>();
        }

        public string Description { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public Category Category { get; private set; }

        public string ImageUrl { get; private set; }

        public IReadOnlyCollection<Comment> Comments => this.comments.ToList().AsReadOnly();
        public IReadOnlyCollection<Like> Likes => this.likes.ToList().AsReadOnly();

        public void AddComment(string description, string imageUrl, string userId)
        {
            var comment = new Comment(description, imageUrl, userId);
            this.comments.Add(comment);
        }

        public void AddLike(bool isLike, string userId)
        {
            var like = new Like(isLike, userId);
            this.likes.Add(like);
        }

        public Like ChangeLike(Like like)
        {
            return like.ChangeLike();
        }

        public Post UpdateImageUrl(string imageUrl)
        {
            this.ValidateImageUrl(imageUrl);
            this.ImageUrl = imageUrl;

            return this;
        }

        public Comment DeleteComment(Comment comment)
        {
            this.comments.Remove(comment);
            return comment;
        }

        public Comment UpdateComment(
            Comment comment,
            string description,
            string imageUrl)
        {
            comment.UpdateDescription(description);
            comment.UpdateImageUrl(imageUrl);
            return comment;
        }



        public Post UpdateDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        public Post UpdateCategory(Category category)
        {
            this.ValidateCategory(category);
            this.Category = category;

            return this;
        }

        private void ValidateImageUrl(string imageUrl)
            => Guard.ForValidUrl<InvalidPostException>(
                imageUrl,
                nameof(this.ImageUrl));

        public void ValidateDescription(string description)
           => Guard.ForStringLength<InvalidPostException>(
               description,
               MinDescriptionLength,
               MaxDescriptionLength,
               nameof(this.Description));

        private void ValidateCategory(Category category)
        {
            var categoryName = category?.Name;

            if (AllowedCategories.Any(c => c.Name == categoryName))
            {
                return;
            }

            var allowedCategoryNames = string.Join(", ", AllowedCategories.Select(c => $"'{c.Name}'"));

            throw new InvalidPostException($"'{categoryName}' is not a valid category. Allowed values are: {allowedCategoryNames}.");
        }
    }
}
