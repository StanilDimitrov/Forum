using Forum.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalSystem.Infrastructure.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.PublicUser)
                .WithOne()
                .HasForeignKey<User>("PublicUserId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
