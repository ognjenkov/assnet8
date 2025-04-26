using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Organizations.Request;
using assnet8.Dtos.Organizations.Response;
using assnet8.Services.Images;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
[Route("organizations")]
public class OrganizationsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IImageService _imageService;

    public OrganizationsController(AppDbContext dbContext, IImageService imageService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
    }

    [VerifyRoles(Roles.Creator, Roles.OrganizationOwner, Roles.Organizer, Roles.ServiceProvider)]
    [HttpPatch]
    public IActionResult UpdateOrganization()
    {
        return Ok("Update organization");
    }

    [VerifyRoles(Roles.Creator, Roles.OrganizationOwner, Roles.Organizer, Roles.ServiceProvider)]
    [HttpDelete]
    public IActionResult DeleteOrganization()
    {
        return Ok("Delete organization");
    }

    [HttpGet("card/{OrganizationId}")]
    public async Task<IActionResult> GetOrganizationCard([FromRoute] GetOrganizationCardRequestDto request)
    {
        var organization = await _dbContext.Organizations
                            .Where(o => o.Id == request.OrganizationId)
                            .Include(o => o.LogoImage)
                            .Include(o => o.Fields)
                            .ThenInclude(f => f.ThumbnailImage)
                            .Include(o => o.Games)
                            .Include(o => o.Services)
                            .ThenInclude(s => s.ThumbnailImage)
                            .Include(o => o.User)
                            .ThenInclude(u => u!.ProfileImage)
                            .Include(o => o.Team)
                            .ThenInclude(t => t!.LogoImage)
                            .FirstOrDefaultAsync();

        if (organization == null) return NotFound("Organization not found");

        return Ok(new GetOrganizationCardResponseDto
        {
            Id = organization.Id,
            Name = organization.Name,
            CreateDateTime = organization.CreateDateTime,
            LogoImage = organization.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(organization.LogoImage.Id)
            },
            Fields = organization.Fields.Select(f => new FieldSimpleDto
            {
                Id = f.Id,
                Name = f.Name,
                GoogleMapsLink = f.GoogleMapsLink,
                ThumbnailImage = f.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(f.ThumbnailImage.Id)
                }
            }).ToList(),
            Games = organization.Games.Select(g => new GameSimpleDto
            {
                Id = g.Id,
                Title = g.Title,
                StartDateTime = g.StartDateTime
            }).ToList(),
            Services = organization.Services.Select(s => new ServiceSimpleDto
            {
                Id = s.Id,
                Title = s.Title,
                ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(s.ThumbnailImage.Id)
                }
            }).ToList(),
            Team = organization.Team == null ? null : new TeamSimpleDto
            {
                Id = organization.Team.Id,
                Name = organization.Team.Name,
                LogoImage = organization.Team.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(organization.Team.LogoImage.Id)
                }
            },
            User = organization.User == null ? null : new UserSimpleDto
            {
                Id = organization.User.Id,
                Username = organization.User.Username,
                ProfileImage = organization.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(organization.User.ProfileImage.Id)
                }
            }
        });
    }

    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.ServiceProvider)]
    [HttpPost("team")]
    public async Task<IActionResult> CreateTeamOrganization()
    {
        // NOTE u ovoj ruti cu malo drugacije da postupim, verovacu jwt-ju i samo cu da ga napraivm
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return Unauthorized();
        }
        var team = await _dbContext.Teams
            .FirstOrDefaultAsync(t => t.Memberships.Any(m => m.UserId == parsedUserId));
        if (team == null) return NotFound("Team not found");

        var organization = new Organization
        {
            Name = team.Name,
            UserId = team.CreatorId,
            TeamId = team.Id,
            LogoImageId = team.LogoImageId
        };
        await _dbContext.Organizations.AddAsync(organization);
        await _dbContext.SaveChangesAsync();

        return StatusCode(201, organization.Id);
    }

    [HttpPost("individual")]
    public async Task<IActionResult> CreateIndividualOrganization([FromForm] CreateOrganizationRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                            .Where(u => u.Id == Guid.Parse(userId))
                            .Include(u => u.Membership)
                            .FirstOrDefaultAsync();

        if (user == null) return NotFound("User not found");
        if (user.Organization != null) return BadRequest("User already has an organization");
        if (user.Membership?.TeamId != null) return BadRequest("User already has a team " + user.Membership.TeamId);

        var organization = new Organization
        {
            Name = request.Name,
            UserId = user.Id,
        };
        await _dbContext.Organizations.AddAsync(organization);
        await _dbContext.SaveChangesAsync();

        if (request.OrganizationImage != null)
        {
            try
            {
                var image = await _imageService.UploadImage(user, request.OrganizationImage);
                await _dbContext.Images.AddAsync(image);

                organization.LogoImageId = image.Id;

                await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Failed to upload organization image");
                // throw;
            }
        }

        return StatusCode(201, organization.Id);
    }

}