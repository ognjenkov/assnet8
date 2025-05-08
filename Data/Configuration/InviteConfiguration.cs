using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration;

public class InviteConfiguration : IEntityTypeConfiguration<Invite>
{
    public void Configure(EntityTypeBuilder<Invite> builder)
    {
        builder.HasOne(i => i.User)
        .WithMany(u => u.Invites)
        .HasForeignKey(i => i.UserId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.Team)
        .WithMany(t => t.Invites)
        .HasForeignKey(i => i.TeamId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.CreatedBy)
        .WithMany(u => u.CreatedInvites)
        .HasForeignKey(i => i.CreatedById)
        .OnDelete(DeleteBehavior.NoAction);
    }
}