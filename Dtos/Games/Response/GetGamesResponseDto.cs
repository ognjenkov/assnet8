using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Games.Response;

public class GetGamesResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Title { get; set; }
    public DateTimeOffset CreateDateTime { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public List<Tag> Tags { get; set; } = [];
    public OrganizationSimpleDto? Organization { get; set; }
    public FieldSimpleDto? Field { get; set; }
}