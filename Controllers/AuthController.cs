using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using assnet8.Dtos.Auth;
using assnet8.Services.Auth;
using assnet8.Services.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("auth")]
public class AuthController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IJwtService _jwtService;
    private readonly IImageService _imageService;

    public AuthController(AppDbContext dbContext, IJwtService jwtService, IImageService imageService)
    {
        this._jwtService = jwtService;
        this._dbContext = dbContext;
        this._imageService = imageService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _dbContext.Users
            .Where(u => u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail)
            .Include(u => u.Membership)
                .ThenInclude(m => m!.Roles)
            .Include(u => u.Organization)
            .Include(u => u.ProfileImage)
            .FirstOrDefaultAsync();// ovde si stao, razocaran jer ti rolovi nisu niz
                                   //kako mi ovaj glupi kurac da error a account service ne??

        if (user == null)
        {
            return NotFound("User not found"); // ovo se nikad nece desiti jer sam vec proverio u validaciji
        }

        var passwordHasher = new PasswordHasher<string>();

        if (passwordHasher.VerifyHashedPassword("", user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new { message = "Invalid password" });
        }

        var accessToken = _jwtService.GenerateAccessToken(user, null);
        var refreshTokenApp = _jwtService.GenerateRefreshToken(user, null);
        var refreshTokenCookie = _jwtService.GenerateRefreshToken(user, null);

        user.RefreshTokenApp = refreshTokenApp;
        user.RefreshTokenCookie = refreshTokenCookie;
        user.PersistLogin = request.Persist;

        await _dbContext.SaveChangesAsync();

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            MaxAge = TimeSpan.FromDays(30)
        };

        Response.Cookies.Append("jwt", refreshTokenCookie, cookieOptions);

        var roles = user.Membership?.Roles.ToList() ?? new List<Role>();

        if (user.Organization != null) roles.Add(new Role { Name = "OrganizationOwner" });


        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshTokenApp = refreshTokenApp,
            Username = user.Username,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
            },
            Roles = roles,
            TeamId = user.Membership?.TeamId ?? null,
            OrganizationId = user.Organization?.Id != null ? (user.Membership?.TeamId != null ? null : user.Organization?.Id) : null // svaka organizacija ima kreatora, to nas ne zanima ako korisnik ima tim, zato saljem null
        };
        return Ok(response);
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("jwt", out string? refreshTokenCookie))
        {
            return StatusCode(204);
        }
        var user = await _dbContext.Users
            .Where(u => u.RefreshTokenCookie == refreshTokenCookie)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            Response.Cookies.Delete("jwt");
            return StatusCode(204);
        }

        user.RefreshTokenApp = null;
        user.RefreshTokenCookie = null;
        user.PersistLogin = false;
        await _dbContext.SaveChangesAsync();
        Response.Cookies.Delete("jwt");
        return StatusCode(204);

    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
    {
        if (!Request.Cookies.TryGetValue("jwt", out string? refreshTokenCookie))
        {
            return Unauthorized();
        }
        var user = await _dbContext.Users
            .Where(u => u.RefreshTokenCookie == refreshTokenCookie)
            .Include(u => u.Membership)
                .ThenInclude(m => m!.Roles)
            .Include(u => u.Organization)
            .Include(u => u.ProfileImage)
            .FirstOrDefaultAsync();

        if (user == null) return StatusCode(403, new { message = "User not found" }); // ako ne postoji user sa tim cookiem
        if (user.PersistLogin == false && request.RefreshTokenApp == null) // ako nije persist login request.refreshTokenApp mora da bude prosledjen
        {
            return StatusCode(403, new { message = "user.PersistLogin == false && request.RefreshTokenApp == null" });
        }
        if (user.PersistLogin == false && request.RefreshTokenApp != null && user.RefreshTokenApp != request.RefreshTokenApp) // ako je prosledjen mora da bude na u useru
        { //TODO mislim da ovde trebam da ga izlogujem jer je neko pokusao da refreshuje a vrv nije korisnik, ili mozda tek da izlogujem kad prodje decode i proveru emaila jer je tad vec opasna stvar, znaci neko je def pokusao sa starim keyom da refreshuje
            return StatusCode(403, new { message = "user.PersistLogin == false && request.RefreshTokenApp != null && user.RefreshTokenApp != request.RefreshTokenApp" });
        }

        DecodedRefreshToken? decodedRefreshToken = null;
        try
        {
            decodedRefreshToken = user.PersistLogin ? _jwtService.DecodeRefreshToken(refreshTokenCookie) : _jwtService.DecodeRefreshToken(request.RefreshTokenApp!); // moras ovde error handling da uradiserror handling TODO
        }
        catch (System.Exception)
        {
            return StatusCode(403);
        }

        if (decodedRefreshToken.HashedEmail == null) return StatusCode(403, new { message = "decodedRefreshToken.HashedEmail == null" }); // mora da ima email u refresh tokenu

        var passwordHasher = new PasswordHasher<string>();

        if (passwordHasher.VerifyHashedPassword("", decodedRefreshToken.HashedEmail, user.Email) == PasswordVerificationResult.Failed) // hashovani email iz refresh tokena mora da bude isti kao email u useru
        {
            return StatusCode(403, new { message = "VerifyHashedPassword" });
        }

        var newAccessToken = _jwtService.GenerateAccessToken(user, null);
        var newRefreshTokenApp = _jwtService.GenerateRefreshToken(user, decodedRefreshToken.Expiration);
        var newRefreshTokenCookie = _jwtService.GenerateRefreshToken(user, decodedRefreshToken.Expiration);

        user.RefreshTokenApp = newRefreshTokenApp;
        user.RefreshTokenCookie = newRefreshTokenCookie;

        await _dbContext.SaveChangesAsync();

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            MaxAge = decodedRefreshToken.Expiration - DateTime.UtcNow
        };
        Response.Cookies.Append("jwt", newRefreshTokenCookie, cookieOptions);

        var roles = user.Membership?.Roles.ToList() ?? new List<Role>();

        if (user.Organization != null) roles.Add(new Role { Name = "OrganizationOwner" });

        var response = new LoginResponseDto
        {
            AccessToken = newAccessToken,
            RefreshTokenApp = newRefreshTokenApp,
            Username = user.Username,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
            },
            Roles = roles,
            TeamId = user.Membership?.TeamId ?? null,
            OrganizationId = user.Organization?.Id ?? null
        };

        return Ok(response);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.TeamLeader, Roles.Member, Roles.OrganizationOwner, Roles.ServiceProvider)]
    [HttpGet("get-cookie")]
    public IActionResult GetCookie()
    {
        if (Request.Cookies.TryGetValue("jwt", out string? cookieValue))
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role) // Get all role claims
                .Select(c => c.Value)
                .ToList();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid user token");
            }

            return Ok(new
            {
                CookieValue = cookieValue,
                User = new
                {
                    Id = userId,
                    Roles = roles
                }
            });
        }

        return NotFound("Cookie not found");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequestDto request)
    {
        try
        {
            var passwordHasher = new PasswordHasher<string>();
            string hashedPassword = passwordHasher.HashPassword("", request.Password);

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Username = request.Username,
                Password = hashedPassword
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();


            if (request.ProfileImage != null)
            {
                try
                {
                    var image = await _imageService.UploadImage(user, request.ProfileImage);
                    await _dbContext.Images.AddAsync(image);

                    user.ProfileImageId = image.Id;

                    await _dbContext.SaveChangesAsync();
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed to upload profile image");
                    // throw;
                }

            }

            return StatusCode(201, new { message = $"New user {request.Username} created!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });

            // throw; predobra sintaksa za throwovanje expectiona tacno iz mesta odakle je dosao, kad bih ga uhavtion kao promenljivu putanjju bih sjebao ako bi ga throvovao ponovo
        }
    }
}