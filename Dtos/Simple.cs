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
    public required string Name { get; set; }
    public ImageSimpleDto? LogoImage { get; set; }
}
public class ImageSimpleDto
{
    public required string Url { get; set; }
}
public class ListingSimpleDto
{
    public DateTime RefreshDateTime { get; set; }
    public required ListingStatus Status { get; set; }
    public required string Title { get; set; }
    public ImageSimpleDto? ThumbnailImage { get; set; }
}
public class UserSimpleDto
{
    public required string Username { get; set; }
    public ImageSimpleDto? ProfileImage { get; set; }
}
public class LocationSimpleDto
{
    public required string Region { get; set; }
}