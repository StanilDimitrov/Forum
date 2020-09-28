using Forum.Doman.PublicUsers.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Post;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Infrastructure.PublicUsers.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
              .HasKey(p => p.Id);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
                .Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(MaxUrlLength);

            builder
                .Property(p => p.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            builder
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Comments)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("comments");

            builder
                .HasMany(p => p.Likes)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("likes");
        }
    }
}
