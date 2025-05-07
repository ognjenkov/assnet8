using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using assnet8.Dtos.Memberships.Response;
using assnet8.Dtos.Memberships.Request;
using assnet8.Services.SignalR;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using assnet8.Dtos.Memberships.Respnose;

namespace assnet8.Controllers;

[Authorize]
[Route("memberships")]
public class MembershipsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly INextJsRevalidationService _nextJsRevalidationService;
    private readonly IHubContext<InvitesTeamHub> _invitesTeamHub;
    private readonly IHubContext<InvitesUserHub> _invitesUserHub;

    public MembershipsController(AppDbContext dbContext, INextJsRevalidationService nextJsRevalidationService, IHubContext<InvitesTeamHub> invitesTeamHub, IHubContext<InvitesUserHub> invitesUserHub)
    {
        this._dbContext = dbContext;
        this._nextJsRevalidationService = nextJsRevalidationService;
        this._invitesTeamHub = invitesTeamHub;
        this._invitesUserHub = invitesUserHub;
    }

    [HttpGet("roles")]
    public async Task<ActionResult<IEnumerable<GetRolesResponseDto>>> GetRoles()
    {
        var roles = await _dbContext.Roles.ToListAsync();

        return Ok(roles.Select(r => new GetRolesResponseDto
        {
            Id = r.Id,
            Name = r.Name
        }));
    }

    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPatch("user/roles")]
    public async Task<ActionResult> UpdateUserRoles([FromBody] UpdateUserRolesRequestDto request)
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        if (userGuid == request.UserId) return BadRequest("Modifying your own roles is not supported yet."); //TODO da se napravi

        List<Role> roles = (List<Role>)HttpContext.Items["ValidatedRoles"]!;

        //TODO sta ako ocovek ima rolove vece od tebe i ti njemu menjas, ne bi bilo fer, sta ako je covek pravio servise i ti mu uklanjas to to ne bi bilo fer

        var membership = await _dbContext.Memberships
                            .Where(m => m.Id == request.UserId && m.TeamId == teamGuid)
                            .Include(m => m.Roles)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

        if (membership.Roles.Any(r => r.Name == Roles.Creator) && !roles.Any(r => r.Name == Roles.Creator)) return BadRequest("Creator role can't be removed.");

        membership.Roles.Clear();
        membership.Roles.AddRange(roles);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpDelete("user")]
    public async Task<ActionResult> RemoveUserFromTeam([FromQuery] RemoveUserFromTeamRequestDto request)
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        if (userGuid == request.UserId) return BadRequest("Can't remove yourself, leave team instead.");

        var membership = await _dbContext.Memberships
                            .Where(m => m.Id == request.UserId && m.TeamId == teamGuid)
                            .Include(m => m.Roles)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

        if (membership.Roles.Any(r => r.Name == Roles.Creator)) return BadRequest("Creator can't be removed.");

        //TODO sta ako ima servise povezane, sta ako ima objave preko tima, sta onda?? sta ako kikujes nekoga ko ima vazne rolove - jedini nacin je da owner bude onaj ko je last modifikovao taj objekat, jer onda ako izbacis nekoga i ti modifikujes zalepice se za tebe servis
        // ili mozda da se prebace objave na Creatora to nije losa ideja :D
        _dbContext.Memberships.Remove(membership);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> LeaveTeam()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        if (roles.Any(r => r == Roles.Creator)) return BadRequest("Creator can't leave team, creator must delete team.");//TODO na fornu napravi nekako da je delete tim ako pokusa da izadje creator ....

        var membership = await _dbContext.Memberships
                            .Where(m => m.UserId == userGuid)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");
        //TODO sta ako izadje neko sa servisima sta onda, ako se obrise tim/organizacija moraju i svi propratni servisi, ali ne i gejmovi? ili da gejmovi?
        _dbContext.Memberships.Remove(membership);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("{TeamId}/simple")]
    public async Task<ActionResult<IEnumerable<MembershipSimpleDto>>> GetTeamMembershipsSimple([FromRoute] GetTeamMembershipsSimpleRequestDto request)
    {
        var memberships = await _dbContext.Memberships
                            .Where(m => m.TeamId == request.TeamId)
                            .Include(m => m.Roles)
                            .Include(m => m.User)
                                .ThenInclude(u => u!.ProfileImage)
                            .ToListAsync();

        if (memberships == null) return NotFound("Memberships not found");

        return Ok(memberships.Select(m => new MembershipSimpleDto
        {
            Id = m.Id,
            CreateDateTime = m.CreateDateTime,
            Roles = [],
            User = new UserSimpleDto
            {
                Id = m.UserId,
                Username = m.User!.Username,
                ProfileImage = m.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(m.User.ProfileImage.Id)
                }
            }
        }));
    }


    [AllowAnonymous]
    [HttpGet("{TeamId}/{MembershipId}/simple")]
    public async Task<ActionResult<MembershipSimpleDto>> GetTeamMembershipSimple([FromRoute] GetTeamMembershipSimpleRequestDto request)
    {
        var membership = await _dbContext.Memberships
                            .Where(m => m.Id == request.MembershipId)
                            .Include(m => m.Roles)
                            .Include(m => m.User)
                                .ThenInclude(u => u!.ProfileImage)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

        return Ok(new MembershipSimpleDto
        {
            Id = membership.Id,
            CreateDateTime = membership.CreateDateTime,
            Roles = membership.Roles,
            User = new UserSimpleDto
            {
                Id = membership.UserId,
                Username = membership.User!.Username,
                ProfileImage = membership.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(membership.User.ProfileImage.Id)
                }
            }
        });
    }











    [HttpPost("invites/user")]
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
    [HttpPost("invites/user/accept")]
    public IActionResult AcceptInviteToTeam([FromBody] AcceptInviteToTeamRequestDto request)
    {
        throw new NotImplementedException();
    }
    [HttpPost("invites/user/decline")]
    public IActionResult DeclineInviteToTeam([FromBody] DeclineInviteToTeamRequestDto request)
    {
        //
        throw new NotImplementedException();
    }
    [HttpPost("invites/user/request")]
    public IActionResult RequestToJoinToTeam([FromBody] RequestToJoinTeamRequestDto request)
    {
        //proveri dal je vec u timu,
        throw new NotImplementedException();
    }















    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team")]
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
    [HttpPost("invites/team/accept")]
    public IActionResult AcceptUserRequest([FromBody] AcceptUserRequestRequestDto request)
    {
        // proveri dal je vec u timu
        // promeni njegov status,
        // promeni sve ostale invajtove na declined

        // osvezi kljueceve, napravi obavestenje, revalidate brda ruta, kada budes pravio servise primetices gde se ovakve stvari grupisu, npr kao revalidate i updejtovanje membershipa
        throw new NotImplementedException();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team/decline")]
    public IActionResult DeclineUserRequest([FromBody] DeclineUserRequestRequestDto request)
    {
        // nemoj da brises nego promeni status
        throw new NotImplementedException();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team/invite")]
    public IActionResult InviteUserToTeam([FromBody] InviteUserToTeamRequestDto request)
    {
        //proveri dal je vec u timu,
        // proveri dal si ga vec invajtovao(aktivno)
        throw new NotImplementedException();
    }
}