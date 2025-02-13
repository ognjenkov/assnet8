using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class GamesController : BaseController
{

    [HttpGet]
    public IActionResult GetGames()
    {
        return Ok("Get games");
    }

    [HttpGet("{gameId}")]
    public IActionResult GetGame([FromQuery] string gameId)
    {
        //ako je logged in dobija i prijave, ako ne samo game
        return Ok("Get game:" + gameId);
    }


}