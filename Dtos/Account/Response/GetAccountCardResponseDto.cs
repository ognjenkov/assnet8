using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Account.Response;
public class GetAccountCardResponseDto
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public ImageSimpleDto? ProfileImage { get; set; }
    public List<ServiceSimpleDto> Services { get; set; } = [];
    public OrganizationSimpleDto? Organization { get; set; }
    public MembershipSimpleDto? Membership { get; set; }
}