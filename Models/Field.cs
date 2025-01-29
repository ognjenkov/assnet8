using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Field
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string GoogleMapsLink { get; set; } = string.Empty;
        public Guid LocationId { get; set; } // jedan prema vise relationship (jedan ima guid drugi ima listu)
        public Guid? GalleryId { get; set; } // jedan prema vise relationship (jedan ima guid drugi ima listu)
        public Guid OrganizationId { get; set; } // jedan prema vise relationship (jedan ima guid drugi ima listu)
        public Guid? ThumbnailImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Image? ThumbnailImage { get; set; } // jedan prema jedan - slika zavisi od fielda jer bez fielda ne bi ni postojala
        public Gallery? Gallery { get; set; } // jedan prema jedan - galerija zavisi od fielda
        public List<Game>? Games { get; set; }
        public Location? Location { get; set; }
        public Organization? Organization { get; set; }
    }
}