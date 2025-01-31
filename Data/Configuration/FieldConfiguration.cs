using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasIndex(l => l.ThumbnailImageId).IsUnique();

            // One-to-One: Thumbnail Image (Cascade delete when Field is deleted)
            builder.HasOne(f => f.ThumbnailImage)
                .WithOne(i => i.Field)
                .HasForeignKey<Field>(f => f.ThumbnailImageId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete the ThumbnailImage when Field is deleted

            // One-to-One: Gallery (Cascade delete when Field is deleted)
            builder.HasOne(f => f.Gallery)
                .WithOne()
                .HasForeignKey<Field>(f => f.GalleryId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete the Gallery when Field is deleted

            // Many-to-Many: Games (Restrict delete if there are related Games)
            builder.HasMany(f => f.Games)
                .WithOne(g => g.Field)  // Each Game has exactly one Field
                .HasForeignKey(g => g.FieldId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of Field if associated with any Games
        }
    }
}