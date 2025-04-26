using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TeamRole
{
    Member,
    TeamLeader,
    Creator,
    Organizer,
    ServiceProvider,

}
public class Membership
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public List<Role> Roles { get; set; } = [];
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    public User? User { get; set; }
}