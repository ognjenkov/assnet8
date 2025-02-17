using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using assnet8.Dtos.Account.Response;
using assnet8.Dtos.Account.Request;

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
                Id = l.Id,
                RefreshDateTime = l.RefreshDateTime,
                Status = l.Status,
                Title = l.Title,
                ThumbnailImage = l.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = l.ThumbnailImage.Id.ToString()
                },
            }).ToList(),
            EntriesNumber = user.Entries.Count,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = user.ProfileImage.Id.ToString()
            },
            Membership = user.Membership == null ? null : new MembershipSimpleDto
            {
                CreateDateTime = user.Membership.CreateDateTime,
                Roles = user.Membership.Roles,
                Team = new TeamSimpleDto
                {
                    Id = user.Membership.TeamId,
                    Name = user.Membership.Team!.Name,
                    LogoImage = user.Membership.Team.LogoImage == null ? null : new ImageSimpleDto
                    {
                        Url = user.Membership.Team.LogoImage.Id.ToString()
                    }
                }
            }
        });
    }

    [HttpGet("organization-information")]
    public async Task<IActionResult> GetOrganizationInformation()
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
            CreateDateTime = organization.CreateDateTime,
            LogoImage = organization.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = organization.LogoImage.Id.ToString()
            },
            Team = organization.Team == null ? null : new TeamSimpleDto
            {
                Id = organization.Team.Id,
                Name = organization.Team.Name,
                LogoImage = organization.Team.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = organization.Team.LogoImage.Id.ToString()
                }
            },
            Fields = organization.Fields?.Select(f => new FieldSimpleDto
            {
                Id = f.Id,
                Name = f.Name,
                ThumbnailImage = f.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = f.ThumbnailImage.Id.ToString()
                },
                GoogleMapsLink = f.GoogleMapsLink
            }).ToList() ?? [],
            Games = organization.Games?.Select(g => new GameSimpleDto
            {
                Id = g.Id,
                Title = g.Title ?? "No title",
                StartDateTime = g.StartDateTime,
            }).ToList() ?? [],
            Services = organization.Services?.Select(s => new ServiceSimpleDto
            {
                Id = s.Id,
                Title = s.Title,
                CreatedDateTime = s.CreatedDateTime,
                ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = s.ThumbnailImage.Id.ToString()
                }
            }).ToList() ?? []
        });

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
                Id = team.CreatorId,
                Username = team.Creator!.Username,
                ProfileImage = team.Creator.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = team.Creator.ProfileImage.Id.ToString()
                }
            },
            LogoImage = team.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = team.LogoImage.Id.ToString()
            },
            Memberships = team.Memberships.Select(m => new MembershipSimpleDto
            {
                CreateDateTime = m.CreateDateTime,
                Roles = m.Roles,
                User = new UserSimpleDto
                {
                    Id = m.UserId,
                    Username = m.User!.Username,
                    ProfileImage = m.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = m.User.ProfileImage.Id.ToString()
                    }
                }
            }).ToList(),
            Location = team.Location == null ? null : new LocationSimpleDto
            {
                Id = team.Location.Id,
                Region = team.Location.Region
            }

        });
    }

    [HttpGet("card/{UserId}")]
    public async Task<IActionResult> GetAccountCard([FromRoute] GetAccountCardRequestDto request)
    {
        var user = await _accountService.GetAccountFromUserId(request.UserId);
        if (user == null) return NotFound("User not found");

        return Ok(new GetAccountCardResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = user.ProfileImage.Id.ToString()
            },
            Services = user.Services?.Select(s => new ServiceSimpleDto
            {
                Id = s.Id,
                Title = s.Title,
                CreatedDateTime = s.CreatedDateTime,
                ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = s.ThumbnailImage.Id.ToString()
                }
            }).ToList() ?? [],
            Organization = user.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = user.Organization.Id,
                Name = user.Organization.Name,
                LogoImage = user.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = user.Organization.LogoImage.Id.ToString()
                }
            },
            Membership = user.Membership == null ? null : new MembershipSimpleDto
            {
                CreateDateTime = user.Membership.CreateDateTime,
                Roles = user.Membership.Roles,
                Team = user.Membership.Team == null ? null : new TeamSimpleDto
                {
                    Id = user.Membership.Team.Id,
                    Name = user.Membership.Team.Name,
                    LogoImage = user.Membership.Team.LogoImage == null ? null : new ImageSimpleDto
                    {
                        Url = user.Membership.Team.LogoImage.Id.ToString()
                    }
                },
            }
        });

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