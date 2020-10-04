using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Message;

namespace Forum.Infrastructure.PublicUsers.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                 .HasKey(c => c.Id);

            builder
                .Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(MaxTextLength);

            builder
               .Property(p => p.CreatedOn)
               .IsRequired()
               .HasColumnType("datetime");

            builder
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(m => m.SenderUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
