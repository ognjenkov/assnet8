using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class ListingsController : BaseController
{

    [HttpGet]
    public IActionResult GetListings()
    {
        return Ok("Get listings");
    }

    [HttpGet("{listingId}")]
    public IActionResult GetListing([FromQuery] string listingId)
    {
        return Ok("Get listing:" + listingId);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateListing()
    {
        return Ok("Create listing");
    }

    [Authorize]
    [HttpDelete]
    public IActionResult DeleteListing()
    {
        return Ok("Delete listing");
    }

    [Authorize]
    [HttpPatch]
    public IActionResult UpdateListing()
    {
        return Ok("Update listing");
    }

}