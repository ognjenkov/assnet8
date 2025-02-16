using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
public class OrganizationsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public OrganizationsController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        var userRoles = user.Membership?.Roles.ToList();

        var allowedRoles = new List<string> { Roles.Creator, Roles.Organizer, Roles.ServiceProvider };
        if (userRoles != null && userRoles.Any(role => allowedRoles.Contains(role.Name)))
        {
            return StatusCode(403);
        }
        // ako je u timu proveri da li ima permissione


        return Ok("Create organization");
    }

    [VerifyRoles(Roles.Creator, Roles.OrganizationOwner, Roles.Organizer, Roles.ServiceProvider)]
    [HttpPatch]
    public IActionResult UpdateOrganization()
    {
        return Ok("Update organization");
    }

    [VerifyRoles(Roles.Creator, Roles.OrganizationOwner, Roles.Organizer, Roles.ServiceProvider)]
    [HttpDelete]
    public IActionResult DeleteOrganization()
    {
        return Ok("Delete organization");
    }

    [HttpGet("card/{organizationId}")]
    public IActionResult GetOrganizationCard([FromQuery] string organizationId)
    {
        return Ok("Get organization card" + organizationId);
    }

}
