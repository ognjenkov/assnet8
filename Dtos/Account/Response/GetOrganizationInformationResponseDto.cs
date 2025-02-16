using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Account.Response
{
    public class GetOrganizationInformationResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        public ImageSimpleDto? LogoImage { get; set; }
        public TeamSimpleDto? Team { get; set; }
        public List<FieldSimpleDto> Fields { get; set; } = [];
        public List<GameSimpleDto> Games { get; set; } = [];
        public List<ServiceSimpleDto> Services { get; set; } = [];
    }
}