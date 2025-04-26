using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasIndex(l => l.LogoImageId).IsUnique();
        builder.HasIndex(l => l.CreatorId).IsUnique();

        // // Configure one-to-many relationship with Membership
        // builder.HasMany(t => t.Memberships)
        //        .WithOne(m => m.Team)
        //        .HasForeignKey(m => m.TeamId)
        //        .OnDelete(DeleteBehavior.Cascade); // Delete all Memberships when Team is deleted

        // // Configure one-to-many relationship with Gallery
        // builder.HasMany(t => t.Galleries)
        //        .WithOne(g => g.Team)
        //        .HasForeignKey(g => g.TeamId)
        //        .OnDelete(DeleteBehavior.Cascade); // Delete all Galleries when Team is deleted

        // // Configure one-to-one relationship with LogoImage (foreign key in Team)
        // builder.HasOne(t => t.LogoImage)
        //        .WithOne(i => i.Team) // Assuming Image does not have a navigation property back to Team
        //        .HasForeignKey<Team>(t => t.LogoImageId)
        //        .OnDelete(DeleteBehavior.Cascade); // DELETE LogoImage when Team is deleted

        // // Configure one-to-one relationship with Organization (restrict delete if Organization exists)
        // builder.HasOne(t => t.Organization)
        //        .WithOne(o => o.Team)
        //        .HasForeignKey<Organization>(o => o.TeamId)
        //        .OnDelete(DeleteBehavior.Restrict); // Prevent Team deletion if Organization exists



        builder.HasOne(t => t.Creator)
        .WithOne(u => u.Team)
        .HasForeignKey<Team>(t => t.CreatorId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.LogoImage)
        .WithOne(i => i.Team)
        .HasForeignKey<Team>(t => t.LogoImageId)
        .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.Location)
        .WithMany(i => i.Teams)
        .HasForeignKey(t => t.LocationId)
        .OnDelete(DeleteBehavior.SetNull);

    }
}