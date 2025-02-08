using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public List<Membership>? Memberships { get; set; }
    }
}