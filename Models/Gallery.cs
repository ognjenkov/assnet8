using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Gallery
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; }
        public Guid UserId { get; set; }
        public Guid? FieldId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? ListingId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? ServiceId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid? TeamId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public User? User { get; set; }
        public Field? Field { get; set; }
        public Listing? Listing { get; set; }
        public Service? Service { get; set; }
        public Team? Team { get; set; }
        public List<Image>? Images { get; set; }
    }
}