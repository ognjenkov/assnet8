using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasIndex(m => m.UserId).IsUnique();

            builder.HasMany(m => m.Roles)
                   .WithMany(r => r.Memberships)
                   .UsingEntity<Dictionary<string, object>>(
                       "MembershipRole", // Name of the join table
                       j => j.HasOne<Role>().WithMany().OnDelete(DeleteBehavior.Cascade), // Cascade delete from role side
                       j => j.HasOne<Membership>().WithMany().OnDelete(DeleteBehavior.Cascade) // Cascade delete from membership side
                   );

            builder.HasOne(m => m.User)
            .WithOne(u => u.Membership)
            .HasForeignKey<Membership>(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Team)
            .WithMany(t => t.Memberships)
            .HasForeignKey(m => m.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}