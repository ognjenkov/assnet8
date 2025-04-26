namespace assnet8.Dtos.Locations;

public class GetLocationsResponseDto
{
    public Guid Id { get; set; }
    public required string Registration { get; set; }
    public required string Region { get; set; }
    public List<MunicipalitySimpleDto> Municipalities { get; set; } = [];
}