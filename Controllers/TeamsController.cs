using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class TeamsController : BaseController
{
    [Authorize]
    [HttpPost]
    public IActionResult CreateTeam()
    {
        //ne sme da ima tim ili da ima organizaciju
        return Ok("Create team");
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