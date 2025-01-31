using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasIndex(l => l.LogoImageId).IsUnique();

            // Configure one-to-one relationship with LogoImage
            builder.HasOne(o => o.LogoImage)
                   .WithOne(i => i.Organization) // Assuming Image does not have a navigation property back to Organization
                   .HasForeignKey<Organization>(o => o.LogoImageId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete LogoImage when Organization is deleted

            // Configure one-to-many relationship with Game
            builder.HasMany(o => o.Games)
                   .WithOne(g => g.Organization)
                   .HasForeignKey(g => g.OrganizationId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete all Games when Organization is deleted

            // Configure many-to-one relationship with Field (restrict delete if Fields exist)
            builder.HasMany(o => o.Fields)
                   .WithOne(f => f.Organization)
                   .HasForeignKey(f => f.OrganizationId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent Organization deletion if Fields exist

            // Configure many-to-one relationship with Service (restrict delete if Services exist)
            builder.HasMany(o => o.Services)
                   .WithOne(s => s.Organization)
                   .HasForeignKey(s => s.OrganizationId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent Organization deletion if Services exist
        }
    }
}