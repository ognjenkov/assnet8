using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class ImagesController : BaseController
{
    [HttpGet("{imageId}")]
    public IActionResult GetImage([FromQuery] string imageId)
    {
        return Ok("Get image:" + imageId);
    }
}