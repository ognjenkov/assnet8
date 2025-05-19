using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Games.Response;

public class GetGameResponseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTimeOffset CreateDateTime { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public string? Description { get; set; }
    public OrganizationSimpleDto? Organization { get; set; }
    public FieldSimpleDto? Field { get; set; }
    public List<Tag> Tags { get; set; } = [];
    public int MaxTotal { get; set; }
    public int MaxRent { get; set; }
    public bool OutsourceEntries { get; set; }
    public string? OutsourceEntriesInstructions { get; set; }
}