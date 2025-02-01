using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
       //      // Configure one-to-one relationship with Field
       //      builder.HasOne(g => g.Field)
       //             .WithOne(f => f.Gallery) // Field has a navigation property back to Gallery
       //             .HasForeignKey<Field>(f => f.GalleryId) // Field has GalleryId as the foreign key
       //             .OnDelete(DeleteBehavior.SetNull); // Set GalleryId to null in Field when Gallery is deleted

       //      // Configure one-to-one relationship with Service
       //      builder.HasOne(g => g.Service)
       //             .WithOne(s => s.Gallery) // Service has a navigation property back to Gallery
       //             .HasForeignKey<Service>(s => s.GalleryId) // Service has GalleryId as the foreign key
       //             .OnDelete(DeleteBehavior.SetNull); // Set GalleryId to null in Service when Gallery is deleted

       //      // Configure one-to-one relationship with Listing
       //      builder.HasOne(g => g.Listing)
       //             .WithOne(l => l.Gallery) // Listing has a navigation property back to Gallery
       //             .HasForeignKey<Listing>(l => l.GalleryId) // Listing has GalleryId as the foreign key
       //             .OnDelete(DeleteBehavior.SetNull); // Set GalleryId to null in Listing when Gallery is deleted

       //      // Configure one-to-many relationship with Images
       //      builder.HasMany(g => g.Images)
       //             .WithOne(i => i.Gallery) // Image has a navigation property back to Gallery
       //             .HasForeignKey(i => i.GalleryId) // Image has GalleryId as the foreign key
       //             .OnDelete(DeleteBehavior.Cascade); // Delete all Images when Gallery is deleted

              builder.HasOne(g => g.Team)
                     .WithMany(t => t.Galleries)
                     .HasForeignKey(g => g.TeamId)
                     .OnDelete(DeleteBehavior.NoAction);

              builder.HasOne(g => g.User)
                     .WithMany(u => u.Galleries)
                     .HasForeignKey(g => g.UserId)
                     .OnDelete(DeleteBehavior.NoAction);
        }
    }
}