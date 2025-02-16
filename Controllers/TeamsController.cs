using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Teams.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class TeamsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public TeamsController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                                    .Where(u => u.Id == Guid.Parse(userId))
                                    .Include(u => u.Membership)
                                    .Include(u => u.Organization)
                                    .FirstOrDefaultAsync();

        if (user == null) return NotFound("User not found");

        if (user.Organization != null || user.Membership != null) return BadRequest("User already in team/organization");

        var roles = await _dbContext.Roles
                                .Where(r => r.Name == Roles.Creator)
                                .ToListAsync();

        var team = new Team
        {
            Name = request.Name,
            CreatorId = user.Id,
        };
        _dbContext.Teams.Add(team);

        var membership = new Membership
        {
            TeamId = team.Id,
            UserId = user.Id,
            Roles = roles,
        };
        _dbContext.Memberships.Add(membership);

        await _dbContext.SaveChangesAsync();



        return Ok();
    }

    [HttpGet]
    public IActionResult GetTeams()
    {
        return Ok("Get teams");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator)]
    [HttpPatch]
    public IActionResult UpdateTeam()
    {
        return Ok("Create team");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator)]
    [HttpDelete]
    public IActionResult DeleteTeam()
    {
        return Ok("delete team");
    }

    [HttpGet("{teamId}")]
    public IActionResult GetTeam([FromQuery] string teamId)
    {
        //na frontu proveravas da li je clan i dal ima role i onda ce pisati edit dugme, mislim barem
        return Ok("Get team " + teamId);
    }

    [HttpGet("members/{teamId}")]
    public IActionResult GetTeamMembers([FromQuery] string teamId)
    {
        return Ok("Get team members " + teamId);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.TeamLeader)]
    [HttpPatch("members/add")]
    public IActionResult AddTeamMember()
    {
        return Ok("Add team member");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.TeamLeader)]
    [HttpPatch("members/remove")]
    public IActionResult RemoveTeamMember()
    {
        return Ok("Remove team member");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.TeamLeader)]
    [HttpPatch("members/update")]
    public IActionResult UpdateTeamMember()
    {
        return Ok("Update team members");
    }
}