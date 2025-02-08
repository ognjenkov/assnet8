using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace assnet8.Services.Auth
{
    public class JwtService : IJwtService
    {
        private string? _accessTokenSecret;
        private string? _refreshTokenSecret;
        private string? _issuer;
        private string? _audience;

        public JwtService(IConfiguration configuration)
        {
            _accessTokenSecret = configuration["Jwt:ACCESS_TOKEN_SECRET"];
            _refreshTokenSecret = configuration["Jwt:REFRESH_TOKEN_SECRET"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }
        public string GenerateAccessToken(User user)
        {
            if (string.IsNullOrWhiteSpace(_accessTokenSecret)) throw new Exception("Unable to generate access token");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("role", user.Membership != null ? user.Membership.Role.ToString() : ""),
                new Claim("organizationOwner", user.Organization != null ? "true" : "false")
            };

            var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10), // Token expires in 10 minutes
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken(User user)
        {
            if (string.IsNullOrWhiteSpace(_refreshTokenSecret)) throw new Exception("Unable to generate refresh token");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var passwordHasher = new PasswordHasher<string>();
            string hashedEmail = passwordHasher.HashPassword("", user.Email);
            var claims = new[]
            {
                new Claim("email", hashedEmail),
            };

            var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}