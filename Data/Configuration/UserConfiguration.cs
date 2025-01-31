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
            // Configure one-to-one relationship with ProfileImage
            builder.HasOne(u => u.ProfileImage)
                   .WithOne(i => i.ProfileImageUser) // Use the new navigation property
                   .HasForeignKey<User>(u => u.ProfileImageId)
                   .OnDelete(DeleteBehavior.SetNull); // Set ProfileImageId to null when Image is deleted

            // Configure one-to-many relationship with UploadedImages
            builder.HasMany(u => u.UploadedImages)
                   .WithOne(i => i.UploadedImagesUser) // Use the new navigation property
                   .HasForeignKey(i => i.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete all UploadedImages when User is deleted

            // One-to-One: Membership (CASCADE)
            builder.HasOne(u => u.Membership)
                .WithOne(m => m.User)
                .HasForeignKey<Membership>(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-One: Organization (RESTRICT)
            builder.HasOne(u => u.Organization)
                .WithOne(o => o.User)
                .HasForeignKey<Organization>(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-One: Team (RESTRICT)
            builder.HasOne(u => u.Team)
                .WithOne(t => t.Creator)
                .HasForeignKey<Team>(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Listings (CASCADE)
            builder.HasMany(u => u.Listings)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Galleries (CASCADE)
            builder.HasMany(u => u.Galleries)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Entries (CASCADE)
            builder.HasMany(u => u.Entries)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Services (RESTRICT)
            builder.HasMany(u => u.Services)
                .WithOne(s => s.CreatedByUser)
                .HasForeignKey(s => s.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}