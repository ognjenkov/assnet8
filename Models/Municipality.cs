using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Municipality
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
    }
}