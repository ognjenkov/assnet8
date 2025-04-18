using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Fields.Response
{
    public class GetFieldResponseDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string GoogleMapsLink { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ImageSimpleDto? ThumbnailImage { get; set; } // jedan prema jedan - slika zavisi od fielda jer bez fielda ne bi ni postojala
        public GallerySimpleDto? Gallery { get; set; } // jedan prema jedan - galerija zavisi od fielda
        public List<GameSimpleDto>? Games { get; set; }
        public LocationSimpleDto? Location { get; set; }
        public OrganizationSimpleDto? Organization { get; set; }
        public UserSimpleDto? User { get; set; }
    }
}