using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public DateTime CreateDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? LengthTime { get; set; }
        public Guid OrganizationId { get; set; } // zavisi od organizacije
        public Guid FieldId { get; set; }
        public List<Tag>? Tags { get; set; }
        public Organization? Organization { get; set; }
        public Field? Field { get; set; }
    }
}