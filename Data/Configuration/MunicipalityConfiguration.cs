using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.HasOne(m => m.Location)
                   .WithMany(l => l.Municipalities)
                   .HasForeignKey(m => m.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}