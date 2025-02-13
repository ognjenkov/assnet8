using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class ServicesController : BaseController
{
    [HttpGet]
    public IActionResult GetServices()
    {
        return Ok("Get services");
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpPost]
    public IActionResult CreateService()
    {
        return Ok("Create service");
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpDelete]
    public IActionResult DeleteService()
    {
        return Ok("Create service");
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpPatch]
    public IActionResult UpdateService()
    {
        return Ok("Create service");
    }

    [HttpGet("{serviceId}")]
    public IActionResult GetService([FromQuery] string serviceId)
    {
        return Ok("Get service:" + serviceId);
    }


}