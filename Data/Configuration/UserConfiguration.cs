using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(l => l.ProfileImageId).IsUnique();
            builder.HasIndex(u => u.RefreshTokenApp).IsUnique();
            builder.HasIndex(u => u.RefreshTokenCookie).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();
           

            builder.HasOne(u => u.ProfileImage)
            .WithOne(i => i.ProfileImageUser)
            .HasForeignKey<User>(u => u.ProfileImageId)
            .OnDelete(DeleteBehavior.NoAction);


        }
    }
}