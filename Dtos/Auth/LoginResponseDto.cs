using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace assnet8.Dtos.Auth;

public class LoginResponseDto
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public ImageSimpleDto? ProfileImage { get; set; }
    public List<Role>? Roles { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshTokenApp { get; set; }
    public Guid? TeamId { get; set; }
    public Guid? OrganizationId { get; set; }
}