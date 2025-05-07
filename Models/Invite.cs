using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InviteStatus
{
    Requested,
    Invited,
    Fullfilled,
}
public class Invite
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required Guid TeamId { get; set; }
    public bool Accepted { get; set; } = false;
    public required InviteStatus Status { get; set; }
    public required Guid CreatedById { get; set; }
    public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    public DateTime? ResponseDateTime { get; set; } = null;
    public Team? Team { get; set; }
    public User? User { get; set; }
    public User? CreatedBy { get; set; }
    public string? Message { get; set; }
    public string? Response { get; set; }
}
