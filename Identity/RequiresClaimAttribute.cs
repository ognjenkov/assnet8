using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace assnet8.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresClaimAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _claimName;
    private readonly string _claimValue;

    public RequiresClaimAttribute(string claimName, string claimValue)
    {
        _claimName = claimName;
        _claimValue = claimValue;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.HasClaim(_claimName, _claimValue))
        {
            context.Result = new ForbidResult();
        }
    }
}

//ovo se koristi kao 
//[Authorize]
//[RequiresClaim(IdentityData.AdminUserClaimName, "true")]


//sidenote: brate ovako lagano mogu da napravim svoje custom proveratore, samo moram tacno da proverim sta je context.HttpContext.User i context.HttpContext.User.HasClaim