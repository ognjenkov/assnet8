using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class FieldsController : BaseController
{

    [HttpGet]
    public IActionResult GetFields()
    {
        return Ok("Get fields");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.TeamLeader, Roles.Member, Roles.OrganizationOwner, Roles.ServiceProvider)]
    [HttpGet("owned")]
    public IActionResult GetOwnedFields()
    {
        return Ok("Get owned fields");
    }

    [HttpGet("{fieldId}")]
    public IActionResult GetField([FromQuery] string fieldId)
    {
        //vratices id organizacije, na frontu ce se proveriti dal je taj id jednak sa idjem tvoje organizacije onda ce da se provere tvoji rolovi i onda ces ima dugme edit ili delete itd...
        return Ok("Get field:" + fieldId);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPost]
    public IActionResult CreateField()
    {
        return Ok("Create field");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpDelete]
    public IActionResult DeleteField()
    {
        return Ok("Delete field");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPatch]
    public IActionResult UpdateField()
    {
        return Ok("Update field");
    }

}