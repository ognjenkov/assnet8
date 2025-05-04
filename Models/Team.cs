using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models;

public class Team
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public Guid CreatorId { get; set; }
    public Guid? LogoImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
    public Guid? LocationId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
    public List<Gallery> Galleries { get; set; } = [];
    public Organization? Organization { get; set; } // u slucaju da tim ima organizaciju prvno nastaje tim - zato je guid u orgu
    public User? Creator { get; set; }
    public Image? LogoImage { get; set; }
    public List<Invite> Invites { get; set; } = [];
    public List<Membership> Memberships { get; set; } = [];
    public Location? Location { get; set; }
}