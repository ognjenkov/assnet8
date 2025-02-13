using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Registration { get; set; }
        public required string Region { get; set; }
        public List<Municipality> Municipalities { get; set; } = [];
        public List<Field> Fields { get; set; } = [];
        public List<Listing> Listings { get; set; } = [];
        public List<Organization> Organizations { get; set; } = [];
        public List<Service> Services { get; set; } = [];
        public List<Team> Teams { get; set; } = [];
    }
}