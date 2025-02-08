using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace assnet8.Dtos.Auth
{
    public class LoginResponseDto
    {
        public required string Username { get; set; }
        public Image? ProfileImage { get; set; }
        public TeamRole? Role { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshTokenApp { get; set; }
        public required bool OrganizationOwner { get; set; } = false;
    }
}