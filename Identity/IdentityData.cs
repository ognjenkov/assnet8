using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Identity
{
    public class IdentityData
    {
        public const string AdminUserClaimName = "admin"; //json web token concern
        public const string AdminUserPolicyName = "Admin"; //application concern

        // ono sto se enkriptuje u jwt tokenu bi trebalo da izgleda ovako:
        // {
        //    "userid": 123,
        //    "customClaims": {
        //        "admin": true
        //    }
        // }
        //
        //
        //implementira se tako sto se stavi ovaj atribut
        //u program.cs
        //builder.Services.AddAuthorization(options => {
        //    options.AddPolicy(IdentityData.AdminUserPolicyName, policy => 
        //        policy.RequireClaim(IdentityData.AdminUserClaimName, "true"));
        //});

        //[Authorize(Policy = IdentityData.AdminUserPolicyName)]


    }
}