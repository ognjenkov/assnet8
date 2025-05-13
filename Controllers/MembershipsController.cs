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
                            .Where(m => m.UserId == request.UserId && m.TeamId == teamGuid)
                            .Include(m => m.Roles)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

        if (membership.Roles.Any(r => r.Name == Roles.Creator) && !roles.Any(r => r.Name == Roles.Creator)) return BadRequest("Creator role can't be removed.");
        if (membership.Roles.Any(r => r.Name == Roles.OrganizationOwner) && !roles.Any(r => r.Name == Roles.OrganizationOwner)) return BadRequest("OrganizationOwner role can't be removed.");

        membership.Roles.Clear();
        membership.Roles.AddRange(roles);

        await _dbContext.SaveChangesAsync();

        await _nextJsRevalidationService.RevalidateTagAsync($"membership-{membership.Id}-simple");

        return NoContent();
    }

    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpDelete("user/kick")]
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
                            .Where(m => m.UserId == request.UserId && m.TeamId == teamGuid)
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



    [AllowAnonymous]
    [HttpGet("{TeamId}/simple")]
    public async Task<ActionResult<IEnumerable<MembershipSimpleDto>>> GetTeamMembershipsSimple([FromRoute] GetTeamMembershipsSimpleRequestDto request)
    {
        var memberships = await _dbContext.Memberships
                            .Where(m => m.TeamId == request.TeamId)
                            .AsSplitQuery()
                            .Include(m => m.Roles)
                            .Include(m => m.User)
                                .ThenInclude(u => u!.ProfileImage)
                            .ToListAsync();

        if (memberships == null) return NotFound("Memberships not found");

        return Ok(memberships.Select(m => new MembershipSimpleDto
        {
            Id = m.Id,
            CreateDateTime = m.CreateDateTime,
            Roles = m.Roles,
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
                            .AsSplitQuery()
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


}