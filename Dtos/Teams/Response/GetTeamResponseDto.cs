using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Teams.Response
{
    public class GetTeamResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        public List<GallerySimpleDto> Galleries { get; set; } = [];
        public OrganizationSimpleDto? Organization { get; set; }
        public UserSimpleDto? Creator { get; set; }
        public ImageSimpleDto? LogoImage { get; set; }
        public List<MembershipSimpleDto> Memberships { get; set; } = [];
        public LocationSimpleDto? Location { get; set; }
    }
}