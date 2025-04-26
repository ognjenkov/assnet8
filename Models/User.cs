using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? RefreshTokenApp { get; set; }
    public string? RefreshTokenCookie { get; set; }
    public bool PersistLogin { get; set; } = false;
    public string? GoogleUid { get; set; }
    public bool VerifiedEmail { get; set; } = false;
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public Guid? ProfileImageId { get; set; }
    public List<Gallery> Galleries { get; set; } = [];
    public List<Service> Services { get; set; } = [];
    public List<Listing> Listings { get; set; } = [];
    public List<Entry> Entries { get; set; } = [];
    public List<Image> UploadedImages { get; set; } = [];
    public Image? ProfileImage { get; set; }
    public Organization? Organization { get; set; }
    public Team? Team { get; set; }
    public Membership? Membership { get; set; }
}