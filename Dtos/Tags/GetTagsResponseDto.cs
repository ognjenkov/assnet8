namespace assnet8.Dtos.Tags;

public class GetTagsResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public TagType Type { get; set; }
}
