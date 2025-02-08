using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assnet8.Dtos.Auth;
using assnet8.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers
{
    public class AuthController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext dbContext, IJwtService jwtService)
        {
            this._jwtService = jwtService;
            this._dbContext = dbContext;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = _dbContext.Users
                .Where(u => u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail)
                .Include(u => u.Membership)
                .Include(u => u.Organization)
                .Include(u => u.ProfileImage)
                .FirstOrDefault();// ovde si stao, razocaran jer ti rolovi nisu niz

            if (user == null)
            {
                return NotFound("User not found"); // ovo se nikad nece desiti jer sam vec proverio u validaciji
            }

            var passwordHasher = new PasswordHasher<string>();

            if (passwordHasher.VerifyHashedPassword("", user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid password" });
            }

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshTokenApp = _jwtService.GenerateRefreshToken(user);
            var refreshTokenCookie = _jwtService.GenerateRefreshToken(user);

            user.RefreshTokenApp = refreshTokenApp;
            user.RefreshTokenCookie = refreshTokenCookie;
            user.PersistLogin = request.RememberMe;

            await _dbContext.SaveChangesAsync();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                MaxAge = TimeSpan.FromDays(30)
            };

            Response.Cookies.Append("jwt", refreshTokenCookie, cookieOptions);

            var response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshTokenApp = refreshTokenApp,
                Username = user.Username,
                ProfileImage = user.ProfileImage,
                Role = user.Membership?.Role,
                OrganizationOwner = user.Organization != null
            };
            return Ok(response);
        }


        [HttpPost("Logout")]
        public IActionResult Logout()
        {

            return Ok("success");
        }
        [HttpPost("Refresh")]
        public IActionResult Refresh()
        {
            return Ok("success");
        }
        [Authorize]
        [HttpGet("get-cookie")]
        public IActionResult GetCookie()
        {
            if (Request.Cookies.TryGetValue("jwt", out string? cookieValue))
            {
                return Ok(new { CookieValue = cookieValue });
            }
            return NotFound("Cookie not found");
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
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

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return StatusCode(201, new { message = $"New user {request.Username} created!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });

                // throw; predobra sintaksa za throwovanje expectiona tacno iz mesta odakle je dosao, kad bih ga uhavtion kao promenljivu putanjju bih sjebao ako bi ga throvovao ponovo
            }
        }
    }
}