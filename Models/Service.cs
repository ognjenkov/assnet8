using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public Guid OrganizationId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid? GalleryId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? ThumbnailImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? LocationId { get; set; }
        public User? CreatedByUser { get; set; }
        public Gallery? Gallery { get; set; }
        public Image? ThumbnailImage { get; set; }
        public List<Tag>? Tags { get; set; }
        public Organization? Organization { get; set; }
        public Location? Location { get; set; }
    }
}