using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Models
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
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
}