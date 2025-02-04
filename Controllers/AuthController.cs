using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assnet8.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController()
        {

        }

        [HttpPost("Login")]
        public IActionResult Login()
        {
            return Ok("success");
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok("success");
        }
        [HttpPost("Refresh")]
        public IActionResult Refresh()
        {
            return Ok("success");
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            return Ok("success");
        }
    }
}