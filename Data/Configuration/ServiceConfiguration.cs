using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasIndex(l => l.ThumbnailImageId).IsUnique();
        builder.HasIndex(l => l.GalleryId).IsUnique();

        // Configure many-to-many relationship with Tag (ServiceTag join table)
        builder.HasMany(s => s.Tags)
               .WithMany(t => t.Services)
               .UsingEntity<Dictionary<string, object>>(
                   "ServiceTag", // Name of the join table
                   j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade), // Cascade delete from Tag side
                   j => j.HasOne<Service>().WithMany().OnDelete(DeleteBehavior.Cascade) // Cascade delete from Service side
               );

        // // Configure one-to-one relationship with Gallery
        // builder.HasOne(s => s.Gallery)
        //        .WithOne(g => g.Service) // Assuming Gallery does not have a navigation property back to Service
        //        .HasForeignKey<Service>(s => s.GalleryId)
        //        .OnDelete(DeleteBehavior.Cascade); // Delete Gallery when Service is deleted

        // // Configure one-to-one relationship with ThumbnailImage
        // builder.HasOne(s => s.ThumbnailImage)
        //        .WithOne(i => i.Service) // Assuming Image does not have a navigation property back to Service
        //        .HasForeignKey<Service>(s => s.ThumbnailImageId)
        //        .OnDelete(DeleteBehavior.Cascade); // Delete ThumbnailImage when Service is deleted

        // // Configure one-to-one relationship with ThumbnailImage
        // builder.HasOne(s => s.CreatedByUser)
        //                 .WithMany(u => u.Services)
        //                 .HasForeignKey(s => s.CreatedByUserId)
        //                 .OnDelete(DeleteBehavior.NoAction); // Delete all UploadedImages when User is deleted

        builder.HasOne(s => s.Organization)
        .WithMany(o => o.Services)
        .HasForeignKey(s => s.OrganizationId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(s => s.CreatedByUser)
        .WithMany(o => o.Services)
        .HasForeignKey(s => s.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(s => s.Gallery)
        .WithOne(g => g.Service)
        .HasForeignKey<Service>(s => s.GalleryId)
        .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(s => s.ThumbnailImage)
        .WithOne(i => i.Service)
        .HasForeignKey<Service>(s => s.ThumbnailImageId)
        .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(s => s.Location)
        .WithMany(l => l.Services)
        .HasForeignKey(s => s.LocationId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}