using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Account.Response
{
    public class GetTeamInformationResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        public required UserSimpleDto Creator { get; set; }
        public ImageSimpleDto? LogoImage { get; set; }
        public required List<MembershipSimpleDto> Memberships { get; set; }
        public LocationSimpleDto? Location { get; set; }
    }
}