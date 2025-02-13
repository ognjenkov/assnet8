using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class LocationsController : BaseController
{

    [HttpGet]
    public IActionResult GetLocations()
    {
        return Ok("Get locations");
    }
}