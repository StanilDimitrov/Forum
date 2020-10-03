using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Infrastructure.PublicUsers.Configurations
{
    public class PublicUserConfiguration : IEntityTypeConfiguration<PublicUser>
    {
        public void Configure(EntityTypeBuilder<PublicUser> builder)
        {
             builder
                .HasKey(pu => pu.Id);

            builder
                .Property(pu => pu.UserName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                  .Property(pu => pu.Email)
                  .IsRequired()
                  .HasMaxLength(MaxEmailLength);

            builder
                .HasMany(pu => pu.Posts)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("posts");

            builder
               .HasMany(pu => pu.InboxMessages)
               .WithOne()
               .Metadata
               .PrincipalToDependent
               .SetField("inboxMessages");

            builder
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<PublicUser>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
