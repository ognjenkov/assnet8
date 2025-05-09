using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Invites.Respnose;

public class GetInvitesResponseDto
{
    public Guid Id { get; set; }
    public TeamSimpleDto? Team { get; set; }
    public UserSimpleDto? User { get; set; }
    public bool Accepted { get; set; } = false;
    public required InviteStatus Status { get; set; }
    public required DateTime CreateDateTime { get; set; }
    public DateTime? ResponseDateTime { get; set; } = null;
    public Guid CreatedById { get; set; }
}