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

    public MembershipsController(AppDbContext dbContext, INextJsRevalidationService nextJsRevalidationService, IHubContext<InvitesTeamHub> invitesTeamHub, IHubContext<InvitesUserHub> invitesUserHub)
    {
        this._dbContext = dbContext;
        this._nextJsRevalidationService = nextJsRevalidationService;
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

        List<Role> roles = (List<Role>)HttpContext.Items["ValidatedRoles"]!; // ovo moze da pukne

        var membership = await _dbContext.Memberships
                            .Where(m => m.Id == request.UserId && m.TeamId == teamGuid)
                            .Include(m => m.Roles)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

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
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

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

        var membership = await _dbContext.Memberships
                            .Where(m => m.UserId == userGuid)
                            .FirstOrDefaultAsync();

        if (membership == null) return NotFound("Membership not found");

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
    public IActionResult GetUserInvites()
    {
        throw new NotImplementedException();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team")]
    public IActionResult GetTeamInvites()
    {
        throw new NotImplementedException();
    }

    [HttpPost("invites/user/accept")]
    public IActionResult AcceptInviteToTeam()
    {
        throw new NotImplementedException();
    }
    [HttpPost("invites/user/decline")]
    public IActionResult DeclineInviteToTeam()
    {
        throw new NotImplementedException();
    }
    [HttpPost("invites/user/request")]
    public IActionResult RequestToJoinToTeam()
    {
        throw new NotImplementedException();
    }

    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team/accept")]
    public IActionResult AcceptUserRequest()
    {
        throw new NotImplementedException();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team/decline")]
    public IActionResult DeclineUserRequest()
    {
        throw new NotImplementedException();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("invites/team/request")]
    public IActionResult InviteUserToTeam()
    {
        throw new NotImplementedException();
    }


}