using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
       public class LocationConfiguration : IEntityTypeConfiguration<Location>
       {
              public void Configure(EntityTypeBuilder<Location> builder)
              {
                     builder.HasMany(l => l.Municipalities)
                            .WithOne(m => m.Location) // Explicitly set the navigation property
                            .HasForeignKey(m => m.LocationId) // Ensure it uses LocationId
                            .OnDelete(DeleteBehavior.Cascade); // Delete Municipalities when Location is deleted

                     // Configure one-to-many relationship with Field
                     builder.HasMany(l => l.Fields)
                            .WithOne(f => f.Location)
                            .HasForeignKey(f => f.LocationId)
                            .OnDelete(DeleteBehavior.SetNull); // Set LocationId to null in Field when Location is deleted

                     // Configure one-to-many relationship with Listing
                     builder.HasMany(l => l.Listings)
                            .WithOne(li => li.Location)
                            .HasForeignKey(li => li.LocationId)
                            .OnDelete(DeleteBehavior.SetNull); // Set LocationId to null in Listing when Location is deleted

                     // Configure one-to-many relationship with Service
                     builder.HasMany(l => l.Services)
                            .WithOne(s => s.Location)
                            .HasForeignKey(s => s.LocationId)
                            .OnDelete(DeleteBehavior.SetNull); // Set LocationId to null in Service when Location is deleted

                     // Configure one-to-many relationship with Team
                     builder.HasMany(l => l.Teams)
                            .WithOne(t => t.Location)
                            .HasForeignKey(t => t.LocationId)
                            .OnDelete(DeleteBehavior.SetNull); // Set LocationId to null in Team when Location is deleted

              }
       }
}