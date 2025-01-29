using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Gallery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public Listing? Listing { get; set; }
        public Service? Service { get; set; }
        public List<Team>? Teams { get; set; }
        public List<Image>? Images { get; set; }
    }
}