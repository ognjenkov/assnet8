using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; }
        public Guid UserId { get; set; } //jedan prema vise zavisi od usera
        public Guid? GalleryId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public User? User { get; set; }
        public Gallery? Gallery { get; set; }
        public Field? Field { get; set; }
        public Listing? Listing { get; set; }
        public Organization? Organization { get; set; }
        public Service? Service { get; set; }
        public Team? Team { get; set; }
    }
}