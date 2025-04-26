using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TagType
{
    Game,
    Listing,
    Service
}
public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public TagType Type { get; set; }
    [JsonIgnore]
    public List<Game> Games { get; set; } = [];
    [JsonIgnore]
    public List<Listing> Listings { get; set; } = [];
    [JsonIgnore]
    public List<Service> Services { get; set; } = [];
}