using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired();

        builder.Property(r => r.Type)
            .IsRequired();


        builder.HasData(
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111197"), Name = "CQB", Type = TagType.Game },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111196"), Name = "Outdoors", Type = TagType.Game },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111195"), Name = "Milsim", Type = TagType.Game },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111194"), Name = "Replica", Type = TagType.Listing },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Gear", Type = TagType.Listing },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111192"), Name = "Uniform", Type = TagType.Listing },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111191"), Name = "Attachment", Type = TagType.Listing },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111180"), Name = "Assault Rifle", Type = TagType.Listing },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111189"), Name = "Pistol", Type = TagType.Listing },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111188"), Name = "AEG Service", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111187"), Name = "GBB Service", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111186"), Name = "HPA Service", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111185"), Name = "Shop", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111184"), Name = "Open games", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111183"), Name = "Private games", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111182"), Name = "Birthdays", Type = TagType.Service },
        new Tag { Id = new Guid("11111111-1111-1111-1111-111111111181"), Name = "Great Renting", Type = TagType.Service }
        );
    }
}