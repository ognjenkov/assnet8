using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder.HasIndex(l => l.ThumbnailImageId).IsUnique();

            // Configure many-to-many relationship with Tag (ListingTag join table)
            builder.HasMany(l => l.Tags)
                   .WithMany(t => t.Listings)
                   .UsingEntity<Dictionary<string, object>>(
                       "ListingTag", // Name of the join table
                       j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade), // Cascade delete from Tag side
                       j => j.HasOne<Listing>().WithMany().OnDelete(DeleteBehavior.Cascade) // Cascade delete from Listing side
                   );

            // Configure one-to-one relationship with Gallery
            builder.HasOne(l => l.Gallery)
                   .WithOne(g => g.Listing) // Gallery does not have a navigation property back to Listing
                   .HasForeignKey<Listing>(l => l.GalleryId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete Gallery when Listing is deleted

            // Configure one-to-one relationship with ThumbnailImage
            builder.HasOne(l => l.ThumbnailImage)
                   .WithOne() // Image does not have a navigation property back to Listing
                   .HasForeignKey<Listing>(l => l.ThumbnailImageId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete Image when Listing is deleted

        }
    }
}