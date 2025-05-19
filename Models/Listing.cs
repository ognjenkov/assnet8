using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ListingCondition
{
    New,
    Used,
    Damaged,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ListingType
{
    Selling,
    Buying,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ListingStatus
{
    Active,
    Inactive,
    Archived,
}
public class Listing
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public DateTime RefreshDateTime { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public required ListingType Type { get; set; }
    public ListingCondition? Condition { get; set; }
    public required ListingStatus Status { get; set; } = ListingStatus.Active;
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Price { get; set; }
    public required string ContactInfo { get; set; }
    public Guid UserId { get; set; }
    public Guid? GalleryId { get; set; }
    public Guid? LocationId { get; set; } // Foreign key to Location
    public Guid? ThumbnailImageId { get; set; } // Foreign key to Image
    public User? User { get; set; }
    public Image? ThumbnailImage { get; set; } // Navigation property to Image
    public List<Tag> Tags { get; set; } = [];
    public Gallery? Gallery { get; set; }
    public Location? Location { get; set; }
}