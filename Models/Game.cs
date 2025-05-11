using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models;
public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Title { get; set; }
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public DateTime StartDateTime { get; set; }
    public string? Description { get; set; }
    public Guid OrganizationId { get; set; } // zavisi od organizacije
    public Guid FieldId { get; set; }
    public List<Entry> Entries { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    public Organization? Organization { get; set; }
    public Field? Field { get; set; }
    public int MaxTotal { get; set; } = 999;
    public int MaxRent { get; set; } = 0;
    public bool OutsourceEntries { get; set; } = false;
    public string? OutsourceEntriesInstructions { get; set; } = null;
}