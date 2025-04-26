using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Listings.Response;

public class GetListingResponseDto
{
    public Guid Id { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime RefreshDateTime { get; set; }
    public required ListingType Type { get; set; }
    public required ListingCondition Condition { get; set; }
    public required ListingStatus Status { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Price { get; set; }
    public required string ContactInfo { get; set; }
    public UserSimpleDto? User { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; } // Navigation property to Image
    public List<Tag> Tags { get; set; } = [];
    public GallerySimpleDto? Gallery { get; set; }
    public LocationSimpleDto? Location { get; set; }
}