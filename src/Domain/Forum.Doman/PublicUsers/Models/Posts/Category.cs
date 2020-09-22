using Forum.Domain.Common.Models;
using Forum.Doman.PublicUsers.Exceptions;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Category;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Doman.PublicUsers.Models.Posts
{
    public class Category : Entity<int>
    {
        internal Category(string name, string description)
        {
            this.Validate(name, description);

            this.Name = name;
            this.Description = description;
        }

        public string Name { get; }

        public string Description { get; }

        private void Validate(string name, string description)
        {
            Guard.ForStringLength<InvalidPostException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.ForStringLength<InvalidPostException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
        }
    }
}
