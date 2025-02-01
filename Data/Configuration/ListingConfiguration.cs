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
            builder.HasIndex(l => l.GalleryId).IsUnique();

            builder.HasMany(l => l.Tags)
                   .WithMany(t => t.Listings)
                   .UsingEntity<Dictionary<string, object>>(
                       "ListingTag", // Name of the join table
                       j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade), // Cascade delete from Tag side
                       j => j.HasOne<Listing>().WithMany().OnDelete(DeleteBehavior.Cascade) // Cascade delete from Listing side
                   );

            builder.HasOne(l => l.Gallery)
            .WithOne(g => g.Listing)
            .HasForeignKey<Listing>(l => l.GalleryId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Location)
            .WithMany(l => l.Listings)
            .HasForeignKey(l => l.LocationId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.User)
            .WithMany(u => u.Listings)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.ThumbnailImage)
            .WithOne(i => i.Listing)
            .HasForeignKey<Listing>(l => l.ThumbnailImageId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}