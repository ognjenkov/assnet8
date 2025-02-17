using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Teams.Response
{
    public class GetTeamsResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ImageSimpleDto? LogoImage { get; set; }
        public LocationSimpleDto? Location { get; set; }
    }
}