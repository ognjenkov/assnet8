using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Listings.Response;

public class GetListingsResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset RefreshDateTime { get; set; }
    public required ListingType Type { get; set; }
    public ListingCondition? Condition { get; set; }
    public required ListingStatus Status { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Price { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; }
    public List<Tag> Tags { get; set; } = [];
    public LocationSimpleDto? Location { get; set; }
}