using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Account.Request;
using assnet8.Dtos.Account.Response;
using assnet8.Services.Account;
using assnet8.Services.Images;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;

[Authorize]
[Route("account")]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    private readonly ICloudImageService _imageService;
    private readonly INextJsRevalidationService _nextJsRevalidationService;
    private readonly AppDbContext _dbContext;
    public AccountController(IAccountService accountService, ICloudImageService imageService, INextJsRevalidationService nextJsRevalidationService, AppDbContext dbContext)
    {
        this._accountService = accountService;
        this._imageService = imageService;
        this._nextJsRevalidationService = nextJsRevalidationService;
        this._dbContext = dbContext;
    }

    [HttpGet("account-information")]
    public async Task<ActionResult<GetAccountInformationResponseDto>> GetAccountInformation()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // return _httpContextAccessor.HttpContext?.User?.Identity?.Name; ovako se nabavlja u servisu
        if (userId == null) return Unauthorized();


        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        return Ok(new GetAccountInformationResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            VerifiedEmail = user.VerifiedEmail,
            CreateDateTime = new DateTimeOffset(user.CreateDateTime, TimeSpan.Zero),
            EntriesNumber = user.Entries.Count,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
            },
            Membership = user.Membership == null ? null : new MembershipSimpleDto
            {
                Id = user.Membership.Id,
                CreateDateTime = new DateTimeOffset(user.Membership.CreateDateTime, TimeSpan.Zero),
                Roles = user.Membership.Roles,
                Team = new TeamSimpleDto
                {
                    Id = user.Membership.TeamId,
                    Name = user.Membership.Team!.Name,
                    LogoImage = user.Membership.Team.LogoImage == null ? null : new ImageSimpleDto
                    {
                        Url = Utils.Utils.GenerateImageFrontendLink(user.Membership.Team.LogoImage.Id)
                    }
                }
            }
        });
    }

    [HttpGet("organization-information")]
    public async Task<ActionResult<GetOrganizationInformationResponseDto>> GetOrganizationInformation()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // return _httpContextAccessor.HttpContext?.User?.Identity?.Name; ovako se nabavlja u servisu
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        var teamId = user.Membership?.TeamId;

        var organization = await _accountService.GetOrganizationFromUserIdOrTeamId(teamId ?? user.Id);

        if (organization == null) return NotFound("Organization not found");

        return Ok(new GetOrganizationInformationResponseDto
        {
            Id = organization.Id,
            Name = organization.Name,
            CreateDateTime = new DateTimeOffset(organization.CreateDateTime, TimeSpan.Zero),
            LogoImage = organization.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(organization.LogoImage.Id)
            },
            Team = organization.Team == null ? null : new TeamSimpleDto
            {
                Id = organization.Team.Id,
                Name = organization.Team.Name,
                LogoImage = organization.Team.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(organization.Team.LogoImage.Id)
                }
            },
            Fields = organization.Fields?.Select(f => new FieldSimpleDto
            {
                Id = f.Id,
                Name = f.Name,
                ThumbnailImage = f.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(f.ThumbnailImage.Id)
                },
                GoogleMapsLink = f.GoogleMapsLink,
                Location = f.Location == null ? null : new LocationSimpleDto
                {
                    Id = f.Location.Id,
                    Region = f.Location.Region
                }
            }).ToList() ?? [],
            Games = organization.Games?.Select(g => new GameSimpleDto
            {
                Id = g.Id,
                Title = g.Title ?? "No title",
                StartDateTime = new DateTimeOffset(g.StartDateTime, TimeSpan.Zero),
            }).ToList() ?? [],
            Services = organization.Services?.Select(s => new ServiceSimpleDto
            {
                Id = s.Id,
                Title = s.Title,
                CreatedDateTime = new DateTimeOffset(s.CreatedDateTime, TimeSpan.Zero),
                ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(s.ThumbnailImage.Id)
                }
            }).ToList() ?? [],
            Creator = new UserSimpleDto
            {
                Id = organization.UserId,
                Username = organization.User!.Username,
                ProfileImage = organization.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(organization.User.ProfileImage.Id)
                }
            }
        });

    }

    [HttpGet("team-information")]
    public async Task<ActionResult<GetTeamInformationResponseDto>> GetTeamInformation()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // return _httpContextAccessor.HttpContext?.User?.Identity?.Name; ovako se nabavlja u servisu
        if (userId == null) return Unauthorized(); //TODO da li zelim ovo ovako da proveramav ili ima bolji nacin

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var team = await _accountService.GetTeamFromUserId(Guid.Parse(userId));

        if (team == null) return NotFound("Team not found");

        return Ok(new GetTeamInformationResponseDto
        {
            Id = team.Id,
            Name = team.Name,
            CreateDateTime = new DateTimeOffset(team.CreateDateTime, TimeSpan.Zero),
            Creator = new UserSimpleDto
            {
                Id = team.CreatorId,
                Username = team.Creator!.Username,
                ProfileImage = team.Creator.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(team.Creator.ProfileImage.Id)
                }
            },
            LogoImage = team.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(team.LogoImage.Id)
            },
            Memberships = team.Memberships.Select(m => new MembershipSimpleDto
            {
                Id = m.Id,
                CreateDateTime = new DateTimeOffset(m.CreateDateTime, TimeSpan.Zero),
                Roles = m.Roles,
                User = new UserSimpleDto
                {
                    Id = m.UserId,
                    Username = m.User!.Username,
                    ProfileImage = m.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = Utils.Utils.GenerateImageFrontendLink(m.User.ProfileImage.Id)
                    }
                }
            }).ToList(),
            Location = team.Location == null ? null : new LocationSimpleDto
            {
                Id = team.Location.Id,
                Region = team.Location.Region
            },
            Galleries = team.Galleries?.Select(g => new GallerySimpleDto
            {
                CreateDateTime = new DateTimeOffset(g.CreateDateTime, TimeSpan.Zero),
                Title = g.Title,
                User = new UserSimpleDto
                {
                    Id = g.UserId,
                    Username = g.User!.Username,
                },
                Images = g.Images?.Select(i => new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(i.Id)
                }).ToList() ?? []

            }).ToList() ?? []

        });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _dbContext.Users
        .AsSplitQuery()
        .Include(u => u.ProfileImage)
        .Include(u => u.Listings)
            .ThenInclude(l => l.Gallery)
                .ThenInclude(g => g!.Images)
        .Include(u => u.Listings)
            .ThenInclude(l => l.ThumbnailImage)
        .Include(u => u.Entries)
        .Include(u => u.Membership)
        .Include(u => u.Organization)
        .FirstOrDefaultAsync(u => u.Id == userGuid);

        if (user == null) return NotFound("User not found");
        if (user.Membership != null) return Unauthorized("User is a member of a team");
        if (user.Organization != null) return Unauthorized("User is an organization member");

        _dbContext.Entries.RemoveRange(user.Entries);

        foreach (var listing in user.Listings)
        {
            if (listing.ThumbnailImage != null)
            {
                await _imageService.DeleteImage(listing.ThumbnailImage);
            }

            if (listing.Gallery != null)
            {
                foreach (var image in listing.Gallery.Images)
                {
                    await _imageService.DeleteImage(image);
                }

                _dbContext.Galleries.Remove(listing.Gallery);
            }
            try
            {
                await Task.WhenAll(
                        _nextJsRevalidationService.RevalidatePathAsync($"/market/{listing.Id}"),
                        _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}-simple"),
                        _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}")
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        await _dbContext.SaveChangesAsync();
        await _nextJsRevalidationService.RevalidateTagAsync("listings");


        try
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("User cannot be deleted due to existing references.");
        }

        if (user.ProfileImage != null)
        {
            await _imageService.DeleteImage(user.ProfileImage);
        }
        await _dbContext.SaveChangesAsync();

        try
        {
            await Task.WhenAll(
                     _nextJsRevalidationService.RevalidateTagAsync($"user-{user.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"user-{user.Id}-simple")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return Ok("Account deleted");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAccount([FromForm] UpdateAccountRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _dbContext.Users
                                    .Include(u => u.ProfileImage)
                                    .FirstOrDefaultAsync(u => u.Id == userGuid);
        if (user == null) return NotFound("User not found");

        if (user.Username == request.Username && user.Name == request.Name && request.ProfileImage == null) return Ok("No changes");

        if (user.Username != request.Username)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest("Username already taken");
            }
            user.Username = request.Username;
        }
        user.Name = request.Name;

        if (request.ProfileImage != null)
        {
            try
            {
                if (user.ProfileImage != null)
                {
                    await _imageService.DeleteImage(user.ProfileImage);

                }
                var image = await _imageService.UploadImage(user, request.ProfileImage);
                await _dbContext.Images.AddAsync(image);

                user.ProfileImageId = image.Id;

            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Failed to upload profile image");
                // throw;
            }
        }
        await _dbContext.SaveChangesAsync();

        await _nextJsRevalidationService.RevalidateTagAsync($"user-{user.Id}");
        await _nextJsRevalidationService.RevalidateTagAsync($"user-{user.Id}-simple");
        return Ok("Update account");
    }

    [HttpPost("password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userGuid);
        if (user == null) return Unauthorized();

        var passwordHasher = new PasswordHasher<string>();

        if (passwordHasher.VerifyHashedPassword("", user.Password, request.OldPassword) == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new { message = "Invalid password" });
        }

        string hashedPassword = passwordHasher.HashPassword("", request.NewPassword);

        user.Password = hashedPassword;
        await _dbContext.SaveChangesAsync();

        return Ok("Update password");
    }
}