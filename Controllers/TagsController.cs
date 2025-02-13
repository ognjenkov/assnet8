using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class TagsController : BaseController
{

    [HttpGet]
    public IActionResult GetTags()
    {
        return Ok("Get tags");
    }
    [HttpGet("service")]
    public IActionResult GetServiceTags()
    {
        return Ok("Get service tags");
    }
    [HttpGet("game")]
    public IActionResult GetGameTags()
    {
        return Ok("Get game tags");
    }
    [HttpGet("listing")]
    public IActionResult GetListingTags()
    {
        return Ok("Get listing tags");
    }
}