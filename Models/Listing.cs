using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ListingCondition {
        New,
        Used,
        Damaged,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ListingType {
        Selling,
        Buying,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ListingStatus {
        Active,
        Inactive,
        Archived,
    }
    public class Listing
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public DateTime RefreshDateTime { get; set; } = DateTime.UtcNow;
        public ListingType Type { get; set; }
        public ListingCondition Condition { get; set; }
        public ListingStatus Status { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid? GalleryId { get; set; }
        public Guid LocationId { get; set; } // jedan prema vise relationship (jedan ima guid drugi ima listu)
        public Guid ThumbnailImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public User? User { get; set; }
        public Image? ThumbnailImage { get; set; } // jedan prema jedan ali image zavisi od listinga tkd je tamo fk
        public List<Tag>? Tags { get; set; }
        public Gallery? Gallery { get; set; }
        public Location? Location { get; set; }
    }
}