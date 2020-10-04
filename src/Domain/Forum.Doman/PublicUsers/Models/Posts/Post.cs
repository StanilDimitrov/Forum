using Forum.Domain.Common;
using Forum.Domain.Common.Models;
using Forum.Domain.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Events.Posts;
using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Users;
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
            Category category)

        {
            this.ValidateDescription(description);
            this.ValidateCategory(category);

            this.Description = description;
            this.Category = category;
            this.CreatedOn = DateTime.Now;
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
        }

        private Post(string description)
        {
            this.Description = description;
            this.CreatedOn = DateTime.Now;
            this.Category = default!;
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
        }

        public string Description { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public Category Category { get; private set; }

        public IReadOnlyCollection<Comment> Comments => this.comments.ToList().AsReadOnly();
        public IReadOnlyCollection<Like> Likes => this.likes.ToList().AsReadOnly();

        public void AddComment(string description, string userId)
        {
            var comment = new Comment(description, userId);
            this.comments.Add(comment);
            //Fire event to new bounded context for notifications
            //TODO Implement notifications bounded context
            this.RaiseEvent(new CommentAddedEvent());
        }

        public Like GetLike(string userId)
        {
            var like = likes.SingleOrDefault(L => L.UserId == userId);
            return like;
        }

        public IReadOnlyCollection<Comment> GetComments()
         => this.comments
             .OrderByDescending(x => x.CreatedOn)
             .ToList();

        public void AddLike(bool isLike, string userId)
        {
            var like = new Like(isLike, userId);
            this.likes.Add(like);
            //Fire event to new bounded context for notifications
            //TODO Implement notifications bounded context
            this.RaiseEvent(new LikeAddedEvent());
        }

        public Like ChangeLike(Like like)
        {
            return like.ChangeLike();
        }

        public Comment DeleteComment(Comment comment)
        {
            this.comments.Remove(comment);
            return comment;
        }

        public Comment UpdateComment(
            Comment comment,
            string description)
        {
            comment.UpdateDescription(description);
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
