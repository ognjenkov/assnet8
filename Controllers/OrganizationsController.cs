using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
public class OrganizationsController : BaseController
{
    [HttpPost]
    public IActionResult CreateOrganization()
    {
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
