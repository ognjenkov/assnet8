using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace assnet8.Models;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    [JsonIgnore] //TODO dobra stvar istrazi zasto sam morao ovo da stavim, neki max depth prilikom serijalizacije, mozda treba da se doda na svaku listu koja se koristi za ef core
    public List<Membership> Memberships { get; set; } = [];
}
public static class Roles
{
    public const string Member = "Member";
    public const string TeamLeader = "TeamLeader";
    public const string Creator = "Creator";
    public const string Organizer = "Organizer";
    public const string ServiceProvider = "ServiceProvider";
    public const string OrganizationOwner = "OrganizationOwner";
}