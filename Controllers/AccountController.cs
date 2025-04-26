using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Account.Request;
using assnet8.Dtos.Account.Response;
using assnet8.Services.Account;
using assnet8.Services.Images;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
[Route("account")]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    private readonly IImageService _imageService;
    public AccountController(IAccountService accountService, IImageService imageService)
    {
        this._accountService = accountService;
        this._imageService = imageService;
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
                    Url = Utils.Utils.GenerateImageFrontendLink(l.ThumbnailImage.Id)
                },
            }).ToList(),
            EntriesNumber = user.Entries.Count,
            ProfileImage = user.ProfileImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
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
                        Url = Utils.Utils.GenerateImageFrontendLink(user.Membership.Team.LogoImage.Id)
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
                    Url = Utils.Utils.GenerateImageFrontendLink(team.Creator.ProfileImage.Id)
                }
            },
            LogoImage = team.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(team.LogoImage.Id)
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
                CreateDateTime = g.CreateDateTime,
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
                Url = Utils.Utils.GenerateImageFrontendLink(user.ProfileImage.Id)
            },
            Services = user.Services?.Select(s => new ServiceSimpleDto
            {
                Id = s.Id,
                Title = s.Title,
                CreatedDateTime = s.CreatedDateTime,
                ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(s.ThumbnailImage.Id)
                }
            }).ToList() ?? [],
            Organization = user.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = user.Organization.Id,
                Name = user.Organization.Name,
                LogoImage = user.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(user.Organization.LogoImage.Id)
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
                        Url = Utils.Utils.GenerateImageFrontendLink(user.Membership.Team.LogoImage.Id)
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