using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Services.Response;

public class GetServicesResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Title { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public UserSimpleDto? CreatedByUser { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; }
    public List<Tag> Tags { get; set; } = [];
    public OrganizationSimpleDto? Organization { get; set; }
    public LocationSimpleDto? Location { get; set; }
}