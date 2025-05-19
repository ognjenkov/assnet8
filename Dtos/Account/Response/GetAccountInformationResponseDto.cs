using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Account.Response;

public class GetAccountInformationResponseDto
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public bool VerifiedEmail { get; set; }
    public DateTimeOffset CreateDateTime { get; set; }
    public List<ListingSimpleDto>? Listings { get; set; }
    public int EntriesNumber { get; set; }
    public ImageSimpleDto? ProfileImage { get; set; }
    public MembershipSimpleDto? Membership { get; set; }
}