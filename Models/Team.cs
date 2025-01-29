using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Guid? LogoImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public List<Gallery>? Galleries { get; set; }
        public Organization? Organization { get; set; } // u slucaju da tim ima organizaciju prvno nastaje tim - zato je guid u orgu
        public Image? LogoImage { get; set; }
        public List<Membership>? Memberships { get; set; }
    }
}