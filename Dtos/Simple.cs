using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Simple;
public class MembershipSimpleDto
{
    public DateTime CreateDateTime { get; set; }
    public List<Role> Roles { get; set; } = [];
    public TeamSimpleDto? Team { get; set; }
    public UserSimpleDto? User { get; set; }
}
public class TeamSimpleDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ImageSimpleDto? LogoImage { get; set; }
}
public class ImageSimpleDto
{
    public required string Url { get; set; }
}
public class ListingSimpleDto
{
    public required Guid Id { get; set; }
    public DateTime RefreshDateTime { get; set; }
    public required ListingStatus Status { get; set; }
    public required string Title { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; }
}
public class UserSimpleDto
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public ImageSimpleDto? ProfileImage { get; set; }
}
public class LocationSimpleDto
{
    public required Guid Id { get; set; }
    public required string Region { get; set; }
}
public class FieldSimpleDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string GoogleMapsLink { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; }
    public LocationSimpleDto? Location { get; set; }
}
public class GameSimpleDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime StartDateTime { get; set; }
}
public class ServiceSimpleDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; }
}
public class OrganizationSimpleDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ImageSimpleDto? LogoImage { get; set; }
}
public class GallerySimpleDto
{
    public required string Title { get; set; }
    public DateTime CreateDateTime { get; set; }
    public UserSimpleDto? User { get; set; }
    public List<ImageSimpleDto> Images { get; set; } = [];
}