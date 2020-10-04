using Forum.Doman.PublicUsers.Models.Posts;
using Forum.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Infrastructure.PublicUsers.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
             .HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseHiLo();
            
            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
                .Property(p => p.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            builder
              .HasOne<User>()
              .WithMany()
              .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
