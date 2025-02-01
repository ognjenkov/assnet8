using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            // // Configure one-to-many relationship with Entries
            // builder.HasMany(g => g.Entries)
            //        .WithOne(e => e.Game) // Entry has a navigation property back to Game
            //        .HasForeignKey(e => e.GameId) // Entry has GameId as the foreign key
            //        .OnDelete(DeleteBehavior.Cascade); // Delete all Entries when Game is deleted

            // Configure many-to-many relationship with Tags
            builder.HasMany(g => g.Tags)
                   .WithMany(t => t.Games) // Tag has a navigation property back to Game
                   .UsingEntity<Dictionary<string, object>>(
                       "GameTag", // Name of the join table
                       j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade), // Configure delete behavior for Tag side
                       j => j.HasOne<Game>().WithMany().OnDelete(DeleteBehavior.Cascade) // Configure delete behavior for Game side
                   );

            builder.HasOne(g => g.Organization)
                   .WithMany(o => o.Games)
                   .HasForeignKey(g => g.OrganizationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Field)
                   .WithMany(f => f.Games)
                   .HasForeignKey(g => g.FieldId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}