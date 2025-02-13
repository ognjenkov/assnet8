using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace assnet8.Identity;
// veoma interesantan kod vredi izuciti detaljno TODO
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VerifyRoles : Attribute, IAuthorizationFilter
    {
        private readonly List<string> _allowedRoles;

        public VerifyRoles(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles.ToList();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var userRoles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            if (!_allowedRoles.Any(role => userRoles.Contains(role)))
            {
                context.Result = new ForbidResult(); // 403 Forbidden
            }
        }
    }
// koristi se
// [Authorize]
// [VerifyRoles(Roles.Member, Roles.TeamLeader)]
// [HttpPost("CreateTeam")]
// public IActionResult CreateTeam() { ... }