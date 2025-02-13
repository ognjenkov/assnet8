using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace assnet8.Services.Auth;
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
    public string GenerateAccessToken(User user, DateTime? expires)
    {
        if (string.IsNullOrWhiteSpace(_accessTokenSecret)) throw new Exception("Unable to generate access token");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        // Add roles individually as separate claims
        if (user.Membership?.Roles != null)
        {
            claims.AddRange(user.Membership.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));
        }

        // Add OrganizationOwner role if applicable
        if (user.Organization != null)
        {
            claims.Add(new Claim(ClaimTypes.Role, "OrganizationOwner"));
        }

        if (expires == null) expires = DateTime.UtcNow.AddMinutes(10);

        var token = new JwtSecurityToken(
        issuer: _issuer,
        audience: _audience,
        claims: claims,
        expires: expires,
        signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken(User user, DateTime? expires)
    {
        if (string.IsNullOrWhiteSpace(_refreshTokenSecret)) throw new Exception("Unable to generate refresh token");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var passwordHasher = new PasswordHasher<string>();
        string hashedEmail = passwordHasher.HashPassword("", user.Email);
        var claims = new[]
        {
                new Claim(ClaimTypes.Email, hashedEmail),
            };

        if (expires == null) expires = DateTime.UtcNow.AddDays(30);

        var token = new JwtSecurityToken(
        issuer: _issuer,
        audience: _audience,
        claims: claims,
        expires: expires,
        signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public DecodedRefreshToken DecodeRefreshToken(string refreshToken) // nije moj kod svtvarno ne znam sta radi
    {
        if (string.IsNullOrWhiteSpace(_refreshTokenSecret))
            throw new Exception("Refresh token secret is not configured.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenSecret));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Validate and parse the token
            var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true, //TODO dodaj ovo nakon sto dodas linkove za front i back u zavisnosti dal je production ili development, ovo ce biti pred kraj aplikacije
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = securityKey,
            }, out var validatedToken);

            // Extract the "email" claim (hashed email)
            var emailClaim = principal.FindFirst(ClaimTypes.Email);

            if (emailClaim == null)
                throw new Exception("Email claim not found.");

            // Extract the "exp" claim (expiration timestamp)
            var expClaim = ((JwtSecurityToken)validatedToken).Payload.Expiration;

            if (expClaim == null)
                throw new Exception("Expiration claim not found.");

            var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expClaim.Value).DateTime;

            return new DecodedRefreshToken
            {
                HashedEmail = emailClaim.Value,
                Expiration = expirationDateTime
            };
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
public class DecodedRefreshToken
{
    public required string HashedEmail { get; set; }
    public required DateTime Expiration { get; set; }
}