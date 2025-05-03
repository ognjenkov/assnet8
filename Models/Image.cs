using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models;

public class Image
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid S3Id { get; set; }
    public required string Title { get; set; }
    public required string Extension { get; set; }
    public string? Url { get; set; }
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    public Guid? GalleryId { get; set; }
    [JsonIgnore]
    public User? ProfileImageUser { get; set; }
    public User? UploadedImagesUser { get; set; }
    [JsonIgnore]
    public Gallery? Gallery { get; set; }
    [JsonIgnore]
    public Field? Field { get; set; }
    [JsonIgnore]
    public Listing? Listing { get; set; }
    [JsonIgnore]
    public Organization? Organization { get; set; }
    [JsonIgnore]
    public Service? Service { get; set; }
    [JsonIgnore]
    public Team? Team { get; set; }
}