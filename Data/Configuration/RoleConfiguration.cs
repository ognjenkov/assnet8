using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired();

            builder.HasIndex(r => r.Name)
                .IsUnique();
                
            builder.HasData(
            new Role { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Member" },
            new Role { Id = new Guid("22222222-2222-2222-2222-222222222222"), Name = "TeamLeader" },
            new Role { Id = new Guid("33333333-3333-3333-3333-333333333333"), Name = "Creator" },
            new Role { Id = new Guid("44444444-4444-4444-4444-444444444444"), Name = "Organizer" },
            new Role { Id = new Guid("55555555-5555-5555-5555-555555555555"), Name = "ServiceProvider" },
            new Role { Id = new Guid("66666666-6666-6666-6666-666666666666"), Name = "OrganizationOwner" }
        );
        }
    }
}