using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Organization
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Guid? LogoImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Guid UserId { get; set; }  // Organization was created by this user
        public Guid? TeamId { get; set; }  // Organization belongs to this team
        public Image? LogoImage { get; set; }
        public List<Field> Fields { get; set; } = [];
        public List<Game> Games { get; set; } = [];
        public List<Service> Services { get; set; } = [];
        public User? User { get; set; }
        public Team? Team { get; set; }
    }
}