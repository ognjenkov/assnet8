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
        public DateTime CreatedDateTime { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? GalleryId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? ThumbnailImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? LocationId { get; set; }
        public User? CreatedBy { get; set; }
        public List<Gallery>? Galleries { get; set; }
        public Image? ThumbnailImage { get; set; }
        public List<Tag>? Tags { get; set; }
        public Organization? Organization { get; set; }
        public Location? Location { get; set; }
    }
}