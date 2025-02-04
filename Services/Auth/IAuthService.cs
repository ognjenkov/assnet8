using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Services.Auth
{
    public interface IAuthService
    {
        Task<IActionResult> Login();
        Task<IActionResult> Logout();
        Task<IActionResult> Refresh();
        Task<IActionResult> Register();
    }
}