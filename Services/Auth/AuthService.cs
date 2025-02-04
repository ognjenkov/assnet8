using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        public AuthService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public Task<IActionResult> Login()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Refresh()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Register()
        {
            throw new NotImplementedException();
        }
    }
}