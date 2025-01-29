using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Organization
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Guid? LogoImageId { get; set; } //jedan prema jedan zavisi od fielda tima itd.. zato ona nosi guid njihov
        public Image? LogoImage { get; set; }
        public List<Field>? Fields { get; set; }
        public List<Game>? Games { get; set; }
        public List<Service>? Services { get; set; }
    }

    public class UserOrganization : Organization
    {
        public Guid UserId { get; set; }  // Organization was created by this user
        public User? User { get; set; }
    }

    public class TeamOrganization : Organization
    {
        public Guid TeamId { get; set; }  // Organization belongs to this team
        public Team? Team { get; set; }
    }
}