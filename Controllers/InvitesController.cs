using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Invites.Request;
using assnet8.Dtos.Invites.Respnose;
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

    [HttpGet("user")]
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
            },
            CreatedById = invite.CreatedById
        }));
    }
    [HttpPost("user/accept")]
    public async Task<IActionResult> AcceptInviteToTeam([FromBody] AcceptInviteToTeamRequestDto request)
    {
        // izvadi user iz tokena
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        // proveri da li vec ima tim u tokenu
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId != null) return BadRequest();

        // nadji invite za usera
        var invite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.Id == request.InviteId && i.UserId == userGuid);
        if (invite == null) return NotFound("Invite not found");

        // proveri da li je invite koji user moze da accepta
        if (invite.Status == InviteStatus.Fullfilled) return BadRequest("Invite already fullfilled");
        if (invite.Status == InviteStatus.Requested) return Unauthorized("You cannot accept your own request");

        // proveri da li membership postoji za svaki slucaj da klikne accept 2 puta za redom....
        var membership = await _dbContext.Memberships.FirstOrDefaultAsync(m => m.UserId == userGuid);
        if (membership != null) return BadRequest("You are already a member of a team");

        // prihvati invite
        invite.Accepted = true;
        invite.ResponseDateTime = DateTime.UtcNow;
        invite.Status = InviteStatus.Fullfilled;

        // odbi sve ostale invite
        var invites = await _dbContext.Invites
               .Where(i => i.UserId == userGuid && i.Status != InviteStatus.Fullfilled && i.Id != invite.Id)
               .ToListAsync();
        foreach (var i in invites)
        {
            invite.Accepted = false;
            invite.ResponseDateTime = DateTime.UtcNow;
            invite.Status = InviteStatus.Fullfilled;
        }
        await _dbContext.SaveChangesAsync();

        var memberRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Member);
        // uclani se u tim
        var newMembership = new Membership
        {
            UserId = userGuid,
            TeamId = invite.TeamId,
            Roles = new List<Role>
            {
                memberRole!
            }
        };
        await _dbContext.Memberships.AddAsync(newMembership);
        await _dbContext.SaveChangesAsync();

        // posalji frontu da osvezi invites
        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("RefreshToken");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        // revalidiraj frontend rute
        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/teams/{invite.TeamId}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"team-{invite.TeamId}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"membership-{newMembership.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"membership-{newMembership.Id}-simple")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return NoContent();
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
        if (invite.Status == InviteStatus.Requested) return Unauthorized("You cannot decline your own request"); // TODO mozda da stavim da moze u smislu da cancel operacija, ali onda bi bio delete ja mislim...

        invite.Accepted = false;
        invite.ResponseDateTime = DateTime.UtcNow;
        invite.Status = InviteStatus.Fullfilled;
        await _dbContext.SaveChangesAsync();

        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        return NoContent();
    }

    [HttpPost("user/request")]
    public async Task<IActionResult> RequestToJoinTeam([FromBody] RequestToJoinTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId != null) return BadRequest(); // da bi ovo stvarno funcionisalo potrebno je da se refreshuje token in time --- TODO znaci moras ili odmah da refreshujes na frontu ili da proveris membership na backu... :(= ) ---- ili 

        var team = await _dbContext.Teams
            .FirstOrDefaultAsync(t => t.Id == request.TeamId);
        if (team == null) return NotFound("Team not found");

        var oldInvite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.UserId == userGuid && i.TeamId == request.TeamId);
        if (oldInvite != null)
        {
            if (oldInvite.Status == InviteStatus.Requested) return BadRequest("Already requested");
            if (oldInvite.Status == InviteStatus.Invited)
            {
                // Accept the invite on user's behalf
                oldInvite.Accepted = true;
                oldInvite.ResponseDateTime = DateTime.UtcNow;
                oldInvite.Status = InviteStatus.Fullfilled;

                // Reject other invites
                var otherInvites = await _dbContext.Invites
                    .Where(i => i.UserId == userGuid && i.Id != oldInvite.Id && i.Status != InviteStatus.Fullfilled)
                    .ToListAsync();
                foreach (var i in otherInvites)
                {
                    i.Accepted = false;
                    i.ResponseDateTime = DateTime.UtcNow;
                    i.Status = InviteStatus.Fullfilled;
                }
                var memberRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Member);

                // Create membership
                var newMembership = new Membership
                {
                    UserId = userGuid,
                    TeamId = oldInvite.TeamId,
                    Roles = new List<Role>
                        {
                            memberRole!
                        }
                };
                await _dbContext.Memberships.AddAsync(newMembership);

                await _dbContext.SaveChangesAsync();

                // Notify clients
                await _invitesUserHub.Clients.Groups(oldInvite.UserId.ToString()).SendAsync("Refetch");
                await _invitesUserHub.Clients.Groups(oldInvite.UserId.ToString()).SendAsync("RefreshToken");
                await _invitesTeamHub.Clients.Groups(oldInvite.TeamId.ToString()).SendAsync("Refetch");

                try
                {
                    await Task.WhenAll(
                        _nextJsRevalidationService.RevalidatePathAsync($"/teams/{oldInvite.TeamId}"),
                        _nextJsRevalidationService.RevalidateTagAsync($"team-{oldInvite.TeamId}"),
                        _nextJsRevalidationService.RevalidateTagAsync($"membership-{newMembership.Id}"),
                        _nextJsRevalidationService.RevalidateTagAsync($"membership-{newMembership.Id}-simple")
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return NoContent();
            }
        }

        var invite = new Invite
        {
            UserId = userGuid,
            TeamId = request.TeamId,
            Status = InviteStatus.Requested,
            CreatedById = userGuid
        };

        await _dbContext.Invites.AddAsync(invite);
        await _dbContext.SaveChangesAsync();

        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        return NoContent();
    }

    [HttpPost("user/delete")]
    public async Task<IActionResult> DeleteRequestToJoinTeam([FromBody] DeleteRequestToJoinTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var invite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.Id == request.InviteId && i.UserId == userGuid);
        if (invite == null) return NotFound("Invite not found");

        if (invite.Status != InviteStatus.Requested) return BadRequest("Invite not requested");

        _dbContext.Invites.Remove(invite);
        await _dbContext.SaveChangesAsync();

        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        return NoContent();
    }



    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpGet("team")]
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
            },
            CreatedById = invite.CreatedById
        }));
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("team/accept")]
    public async Task<IActionResult> AcceptUserRequest([FromBody] AcceptUserRequestRequestDto request)
    {
        // izvadi tim iz tokena
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        // nadji invite
        var invite = await _dbContext.Invites.FirstOrDefaultAsync(i => i.Id == request.InviteId && i.TeamId == teamGuid);
        if (invite == null) return NotFound("Invite not found");

        // provrei dal je invite za tim da prihvati
        if (invite.Status == InviteStatus.Fullfilled) return BadRequest("Invite already fullfilled.");
        if (invite.Status == InviteStatus.Invited) return Unauthorized("You cannot accept your own invitations.");

        // proveri da li je user vec clan tima
        var membership = await _dbContext.Memberships.FirstOrDefaultAsync(m => m.UserId == invite.UserId);
        if (membership != null) return BadRequest("User is a member of a team");

        // prihvati request
        invite.Accepted = true;
        invite.ResponseDateTime = DateTime.UtcNow;
        invite.Status = InviteStatus.Fullfilled;

        // odbi sve ostale invitove za usera
        var invites = await _dbContext.Invites
               .Where(i => i.UserId == invite.UserId && i.Status != InviteStatus.Fullfilled && i.Id != invite.Id)
               .ToListAsync();
        foreach (var i in invites)
        {
            invite.Accepted = false;
            invite.ResponseDateTime = DateTime.UtcNow;
            invite.Status = InviteStatus.Fullfilled;
        }
        await _dbContext.SaveChangesAsync();
        var memberRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Member);

        // dodaj usera u tim
        var newMembership = new Membership
        {
            UserId = invite.UserId,
            TeamId = teamGuid,
            Roles = new List<Role>
            {
                memberRole!
            },
        };
        await _dbContext.Memberships.AddAsync(newMembership);
        await _dbContext.SaveChangesAsync();

        // osvezi timske i korisnicke invitove
        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("RefreshToken");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        // nextjs revalidira front rute vezane za clanstvo u tim,
        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/teams/{invite.TeamId}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"team-{invite.TeamId}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"membership-{newMembership.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"membership-{newMembership.Id}-simple")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return NoContent();
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
        // izvadi tim iz tokena
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized("Teamid");
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized("Teamid");

        // nadji trazenog usera
        var user = await _dbContext.Users
                            .Include(u => u.Membership)
                            .FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null) return NotFound("User not found");

        // proveri dal je user u timu vec
        if (user.Membership?.TeamId != null) return BadRequest("User already in team");

        // TODO sta ako ima vise straih invajtova
        var oldInvite = await _dbContext.Invites
                    .FirstOrDefaultAsync(i => i.UserId == user.Id && i.TeamId == teamGuid);
        if (oldInvite != null)
        {
            if (oldInvite.Status == InviteStatus.Invited) return BadRequest("Invite already sent");
            if (oldInvite.Status == InviteStatus.Requested)
            {

                oldInvite.Accepted = true;
                oldInvite.ResponseDateTime = DateTime.UtcNow;
                oldInvite.Status = InviteStatus.Fullfilled;

                var otherInvites = await _dbContext.Invites
                    .Where(i => i.UserId == user.Id && i.Id != oldInvite.Id && i.Status != InviteStatus.Fullfilled)
                    .ToListAsync();



            }
        }

        var createdById = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (createdById == null) return Unauthorized();
        if (!Guid.TryParse(createdById, out var createdByGuid)) return Unauthorized();

        var invite = new Invite
        {
            UserId = user.Id,
            TeamId = teamGuid,
            Status = InviteStatus.Invited,
            CreatedById = createdByGuid,
        };
        await _dbContext.Invites.AddAsync(invite);
        await _dbContext.SaveChangesAsync();

        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        return NoContent();
    }
    [VerifyRoles([Roles.Creator, Roles.TeamLeader])]
    [HttpPost("team/delete")]
    public async Task<IActionResult> DeleteInviteUserToTeam([FromBody] DeleteInviteUserToTeamRequestDto request)
    {
        var teamId = User.FindFirst("TeamId")?.Value;
        if (teamId == null) return Unauthorized();
        if (!Guid.TryParse(teamId, out var teamGuid)) return Unauthorized();

        var invite = await _dbContext.Invites
            .FirstOrDefaultAsync(i => i.Id == request.InviteId && i.TeamId == teamGuid);
        if (invite == null) return NotFound("Invite not found");

        if (invite.Status != InviteStatus.Invited) return BadRequest("Invite not requested");

        _dbContext.Invites.Remove(invite);
        await _dbContext.SaveChangesAsync();

        await _invitesUserHub.Clients.Groups(invite.UserId.ToString()).SendAsync("Refetch");
        await _invitesTeamHub.Clients.Groups(invite.TeamId.ToString()).SendAsync("Refetch");

        return NoContent();
    }
}