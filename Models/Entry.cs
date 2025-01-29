using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Entry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } // jedan prema vise zato on pamti guid
        public DateTime CreateDateTime { get; set; }
        public int OpNumber { get; set; }
        public int RentNumber { get; set; }
        public string? Message { get; set; }
        public User? User { get; set; }
    }
}