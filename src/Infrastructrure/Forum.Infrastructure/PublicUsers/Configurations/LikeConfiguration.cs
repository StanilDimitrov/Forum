using Forum.Doman.PublicUsers.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.PublicUsers.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            const string id = "Id";

            builder
                .Property<int>(id);

            builder
                .HasKey(id);

            builder
                .Property(p => p.IsLike)
                .IsRequired();
        }
    }
}
