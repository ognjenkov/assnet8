using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
public class GalleriesController : BaseController
{
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPost("field")]
    public IActionResult CreateFieldGallery()
    {
        return Ok("Create field gallery");
    }
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpDelete("field")]
    public IActionResult DeleteFieldGallery()
    {
        return Ok("Delete field gallery");
    }

    [HttpPost("listing")]
    public IActionResult CreateListingGallery()
    {
        return Ok("Create listing gallery");
    }
    [HttpDelete("listing")]
    public IActionResult DeleteListingGallery()
    {
        return Ok("Delete listing gallery");
    }

    [VerifyRoles(Roles.Creator, Roles.ServiceProvider, Roles.OrganizationOwner)]
    [HttpPost("service")]
    public IActionResult CreateServiceGallery()
    {
        return Ok("Create service gallery");
    }

    [VerifyRoles(Roles.Creator, Roles.ServiceProvider, Roles.OrganizationOwner)]
    [HttpDelete("service")]
    public IActionResult DeleteServiceGallery()
    {
        return Ok("Delete service gallery");
    }

    [VerifyRoles(Roles.Creator, Roles.TeamLeader, Roles.OrganizationOwner)]
    [HttpPost("team")]
    public IActionResult CreateTeamGallery()
    {
        return Ok("Create team gallery");
    }

    [VerifyRoles(Roles.Creator, Roles.TeamLeader, Roles.OrganizationOwner)]
    [HttpPatch("team")]
    public IActionResult UpdateTeamGallery()
    {
        return Ok("Create team gallery");
    }

    [VerifyRoles(Roles.Creator, Roles.TeamLeader, Roles.OrganizationOwner)]
    [HttpDelete("team")]
    public IActionResult DeleteTeamGallery()
    {
        return Ok("Create team gallery");
    }

}