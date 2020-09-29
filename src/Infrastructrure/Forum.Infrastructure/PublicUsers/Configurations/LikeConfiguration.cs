using Forum.Doman.PublicUsers.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.PublicUsers.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder
                 .HasKey(c => c.Id);

            builder
                .Property(p => p.IsLike)
                .IsRequired();
        }
    }
}
