using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Invites.Request;
using assnet8.Dtos.Memberships.Respnose;
using assnet8.Services.SignalR;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace assnet8.Controllers;

[Authorize]
[Route("invites")]
public class InvitesController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly INextJsRevalidationService _nextJsRevalidationService;
    private readonly IHubContext<InvitesTeamHub> _invitesTeamHub;
    private readonly IHubContext<InvitesUserHub> _invitesUserHub;

    public InvitesController(AppDbContext dbContext, INextJsRevalidationService nextJsRevalidationService, IHubContext<InvitesTeamHub> invitesTeamHub, IHubContext<InvitesUserHub> invitesUserHub)
    {
        this._dbContext = dbContext;
        this._nextJsRevalidationService = nextJsRevalidationService;
        this._invitesTeamHub = invitesTeamHub;
        this._invitesUserHub = invitesUserHub;
    }

    [HttpPost("user")]
    public async Task<ActionResult<IEnumerable<GetInvitesResponseDto>>> GetUserInvites()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var invites = await _dbContext.Invites
                            .Where(i => i.UserId == userGuid)
                            .Include(i => i.Team)
                                .ThenInclude(t => t!.LogoImage)
                            .OrderByDescending(i => i.CreateDateTime)
                            .ToListAsync();

        return Ok(invites.Select(invite => new GetInvitesResponseDto
        {
            Id = invite.Id,
            Accepted = invite.Accepted,
            CreateDateTime = invite.CreateDateTime,
            ResponseDateTime = invite.ResponseDateTime,
            Status = invite.Status,
            Team = invite.Team == null ? null : new TeamSimpleDto
            {
                Id = invite.Team.Id,
                Name = invite.Team.Name,
                LogoImage = invite.Team.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(invite.Team.LogoImage.Id)
                }
            }
        }));
    }
    [HttpPost("user/accept")]
    public async Task<IActionResult> AcceptInviteToTeam([FromBody] AcceptInviteToTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId != null) return BadRequest();

        var invite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.Id == request.InviteId && i.UserId == userGuid);
        if (invite == null) return NotFound("Invite not found");

        if (invite.Status == InviteStatus.Fullfilled) return BadRequest("Invite already fullfilled");
        if (invite.Status == InviteStatus.Requested) return Unauthorized("You cannot accept your own request");



    }
    [HttpPost("user/decline")]
    public async Task<IActionResult> DeclineInviteToTeam([FromBody] DeclineInviteToTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();
        //
        var invite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.Id == request.InviteId && i.UserId == userGuid);
        if (invite == null) return NotFound("Invite not found");

        if (invite.Status == InviteStatus.Fullfilled) return BadRequest("Invite already fullfilled");

        invite.Status = InviteStatus.Fullfilled;
        invite.Accepted = false;
        invite.ResponseDateTime = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
    [HttpPost("user/request")]
    public async Task<IActionResult> RequestToJoinToTeam([FromBody] RequestToJoinTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId != null) return BadRequest(); // da bi ovo stvarno funcionisalo potrebno je da se refreshuje token in time

        var team = await _dbContext.Teams
            .FirstOrDefaultAsync(t => t.Id == request.TeamId);
        if (team == null) return NotFound("Team not found");

        var oldInvite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.UserId == userGuid && i.TeamId == request.TeamId);
        if (oldInvite != null)
        {
            if (oldInvite.Status == InviteStatus.Requested) return BadRequest("Already requested");
            if (oldInvite.Status == InviteStatus.Invited) { } // TODO uclani se u tim ---------------------------____________________--------------------------

        }

        var invite = new Invite
        {
            UserId = userGuid,
            TeamId = request.TeamId,
            Status = InviteStatus.Requested,
            CreatedById = userGuid
        };

        _dbContext.Invites.Add(invite);
        await _dbContext.SaveChangesAsync();

        //TODO refetch and refresh
        return NoContent();
    }



    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("team")]
    public async Task<ActionResult<IEnumerable<GetInvitesResponseDto>>> GetTeamInvites()
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var invites = await _dbContext.Invites
                            .Where(i => i.TeamId == teamGuid)
                            .Include(i => i.User)
                                .ThenInclude(u => u!.ProfileImage)
                            .OrderByDescending(i => i.CreateDateTime)
                            .ToListAsync();

        return Ok(invites.Select(invite => new GetInvitesResponseDto
        {
            Id = invite.Id,
            Accepted = invite.Accepted,
            CreateDateTime = invite.CreateDateTime,
            ResponseDateTime = invite.ResponseDateTime,
            Status = invite.Status,
            User = invite.User == null ? null : new UserSimpleDto
            {
                Id = invite.User.Id,
                Username = invite.User.Username,
                ProfileImage = invite.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(invite.User.ProfileImage.Id)
                }
            }
        }));
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("team/accept")]
    public async Task<IActionResult> AcceptUserRequest([FromBody] AcceptUserRequestRequestDto request)
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();


        // proveri dal je vec u timu
        // promeni njegov status,
        // promeni sve ostale invajtove na declined

        if (invite.Status == InviteStatus.Fullfilled) return BadRequest("Invite already fullfilled");
        if (invite.Status == InviteStatus.Invited) return Unauthorized("You cannot accept your own request");

        // osvezi kljueceve, napravi obavestenje, revalidate brda ruta, kada budes pravio servise primetices gde se ovakve stvari grupisu, npr kao revalidate i updejtovanje membershipa






    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("team/decline")]
    public async Task<IActionResult> DeclineUserRequest([FromBody] DeclineUserRequestRequestDto request)
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var invite = await _dbContext.Invites
                            .FirstOrDefaultAsync(i => i.Id == request.InviteId && i.TeamId == teamGuid);
        if (invite == null) return NotFound("Invite not found");

        if (invite.Status == InviteStatus.Fullfilled) return BadRequest("Invite already fullfilled");
        if (invite.Status == InviteStatus.Invited) return Unauthorized("You cannot decline your own request"); // TODO mozda da stavim da moze u smislu da cancel operacija, ali onda bi bio delete ja mislim...

        invite.Accepted = false;
        invite.ResponseDateTime = DateTime.UtcNow;
        invite.Status = InviteStatus.Fullfilled;
        await _dbContext.SaveChangesAsync();

        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        return NoContent();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("team/invite")]
    public async Task<IActionResult> InviteUserToTeam([FromBody] InviteUserToTeamRequestDto request)
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var user = await _dbContext.Users
                            .Include(u => u.Membership)
                            .FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null) return NotFound("User not found");

        if (user.Membership?.TeamId != null) return BadRequest("User already in team");


        //proveri dal je vec u timu,
        // proveri dal si ga vec invajtovao(aktivno)









    }
}