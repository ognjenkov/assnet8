using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Organizations.Request;
using assnet8.Dtos.Organizations.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Authorize]
[Route("organizations")]
public class OrganizationsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public OrganizationsController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        var userRoles = user.Membership?.Roles.ToList();

        var allowedRoles = new List<string> { Roles.Creator, Roles.Organizer, Roles.ServiceProvider };
        if (userRoles != null && userRoles.Any(role => allowedRoles.Contains(role.Name)))
        {
            return StatusCode(403);
        }
        if (user.Organization != null) return BadRequest("User already has an organization");

        var organization = new Organization
        {
            Name = request.Name,
            UserId = user.Id,
            TeamId = user.Membership?.TeamId
        };
        // ako je u timu proveri da li ima permissione
        await _dbContext.Organizations.AddAsync(organization);
        await _dbContext.SaveChangesAsync();

        return StatusCode(201, organization.Id);
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

}
