using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration;

public class FieldConfiguration : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.HasIndex(l => l.ThumbnailImageId).IsUnique();
        builder.HasIndex(l => l.GalleryId).IsUnique();

        builder.HasOne(f => f.Location)
               .WithMany(l => l.Fields)
               .HasForeignKey(f => f.LocationId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(f => f.ThumbnailImage)
               .WithOne(i => i.Field)
               .HasForeignKey<Field>(f => f.ThumbnailImageId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(f => f.Gallery)
               .WithOne(g => g.Field)
               .HasForeignKey<Field>(f => f.GalleryId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(f => f.Organization)
               .WithMany(o => o.Fields)
               .HasForeignKey(f => f.OrganizationId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}