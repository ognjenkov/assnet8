using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace assnet8.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VerifyRoles : Attribute, IAuthorizationFilter
    {
        private readonly IEnumerable<TeamRole> _allowedRoles;
        private readonly bool _userOrganizationOwner;

        public VerifyRoles(IEnumerable<TeamRole> allowedRoles, bool userOrganizationOwner = false)
        {
            _allowedRoles = allowedRoles;
            _userOrganizationOwner = userOrganizationOwner;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var roles = user.FindAll(x => x.Type == "role").Select(x => x.Value);

            var hasAccess = roles.Any(role => _allowedRoles.Contains((TeamRole)Enum.Parse(typeof(TeamRole), role)));

            if (!hasAccess && _userOrganizationOwner)
            {
                hasAccess = user.FindFirst("organizationOwner")?.Value == "true"; // .Value uvek vraca string
            }
            //TODO odavde sam skinuo proveravanje organizacije iz baze tkd vrv moze da se skine ovo task i async
            if (!hasAccess)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}