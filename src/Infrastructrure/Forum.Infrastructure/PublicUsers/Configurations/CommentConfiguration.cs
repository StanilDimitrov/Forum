using Forum.Doman.PublicUsers.Models.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Comment;

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
                .HasOne(p => p.PublicUser)
                .WithMany()
                .HasForeignKey("PublicUserId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
