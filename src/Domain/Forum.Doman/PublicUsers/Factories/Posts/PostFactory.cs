﻿using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Doman.PublicUsers.Models.Users;

namespace Forum.Domain.PublicUsers.Factories.Posts
{

    internal class PostFactory : IPostFactory
    {
        private string postDescription = default!;
        private Category postCategory = default!;

        private bool categorySet = false;

        public IPostFactory WithDescription(string description)
        {
            this.postDescription = description;
            return this;
        }

        public IPostFactory WithCategory(string name, string description)
            => this.WithCategory(new Category(name, description));

        public IPostFactory WithCategory(Category category)
        {
            this.postCategory = category;
            this.categorySet = true;
            return this;
        }

        public Post Build()
        {
            if (!this.categorySet)
            {
                throw new InvalidPostException("Category must have a value.");
            }

            return new Post(
                this.postDescription,
                this.postCategory);
        }
    }
}
