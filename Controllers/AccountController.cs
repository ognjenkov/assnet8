using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using assnet8.Dtos.Account.Response;

namespace assnet8.Controllers;
[Authorize]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    [HttpGet("account-information")]
    public async Task<IActionResult> GetAccountInformation()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // return _httpContextAccessor.HttpContext?.User?.Identity?.Name; ovako se nabavlja u servisu
        if (userId == null) return Unauthorized();


        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        return Ok(new GetAccountInformationResponseDto
        {
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            VerifiedEmail = user.VerifiedEmail,
            CreateDateTime = user.CreateDateTime,
            Listings = user.Listings?.Select(l => new ListingSimpleDto
            {
                RefreshDateTime = l.RefreshDateTime,
                Status = l.Status,
                Title = l.Title,
                ThumbnailImage = l.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = l.ThumbnailImage.Url
                },
            }).ToList(),
            EntriesNumber = user.Entries.Count,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = user.ProfileImage.Url
            },
            Membership = user.Membership == null ? null : new MembershipSimpleDto
            {
                CreateDateTime = user.Membership.CreateDateTime,
                Roles = user.Membership.Roles,
                Team = new TeamSimpleDto
                {
                    Name = user.Membership.Team!.Name,
                    LogoImage = user.Membership.Team.LogoImage == null ? null : new ImageSimpleDto
                    {
                        Url = user.Membership.Team.LogoImage.Url
                    }
                }
            }
        });
    }

    [HttpGet("organization-information")]
    public IActionResult GetOrganizationInformation()
    {
        return Ok("Organization information");
    }

    [HttpGet("team-information")]
    public async Task<IActionResult> GetTeamInformation()
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
            CreateDateTime = team.CreateDateTime,
            Creator = new UserSimpleDto
            {
                Username = team.Creator!.Username,
                ProfileImage = team.Creator.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = team.Creator.ProfileImage.Url
                }
            },
            LogoImage = team.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = team.LogoImage.Url
            },
            Memberships = team.Memberships.Select(m => new MembershipSimpleDto
            {
                CreateDateTime = m.CreateDateTime,
                Roles = m.Roles,
                User = new UserSimpleDto
                {
                    Username = m.User!.Username,
                    ProfileImage = m.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = m.User.ProfileImage.Url
                    }
                }
            }).ToList(),
            Location = team.Location == null ? null : new LocationSimpleDto
            {
                Region = team.Location.Region
            }

        });
    }

    [HttpGet("card/{userId}")]
    public IActionResult GetAccountCard([FromQuery] string userId)
    {
        return Ok("User card:" + userId);
    }

    [HttpDelete]
    public IActionResult DeleteAccount()
    {
        return Ok("Account deleted");
    }

    [HttpPatch]
    public IActionResult UpdateAccount()
    {
        return Ok("Update account");
    }
}