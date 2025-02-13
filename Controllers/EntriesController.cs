using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
public class EntriesController : BaseController
{
    [HttpGet("game/{gameId}")]
    public IActionResult GetGameEntries([FromQuery] string gameId)
    {
        return Ok("Game entries:" + gameId);
    }

    [HttpPost]
    public IActionResult CreateEntry()
    {
        return Ok("Create entry");
    }

    [HttpDelete]
    public IActionResult DeleteEntry()
    {
        return Ok("Delete entry");
    }
}