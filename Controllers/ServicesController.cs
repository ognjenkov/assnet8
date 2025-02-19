using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Services.Request;
using assnet8.Dtos.Services.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class ServicesController : BaseController
{
    private readonly AppDbContext _dbContext;
    public ServicesController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _dbContext.Services
                                            .Include(s => s.ThumbnailImage)
                                            .Include(s => s.CreatedByUser)
                                            .ThenInclude(u => u!.ProfileImage)
                                            .Include(s => s.Tags)
                                            .Include(s => s.Organization)
                                            .ThenInclude(o => o!.LogoImage)
                                            .Include(s => s.Location)
                                            .ToListAsync();
        return Ok(services.Select(s => new GetServicesResponseDto
        {
            Id = s.Id,
            Title = s.Title,
            CreatedDateTime = s.CreatedDateTime,
            CreatedByUser = s.CreatedByUser == null ? null : new UserSimpleDto
            {
                Id = s.CreatedByUser.Id,
                Username = s.CreatedByUser.Username,
                ProfileImage = s.CreatedByUser.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = s.CreatedByUser.ProfileImage.Id.ToString()
                }
            },
            ThumbnailImage = s.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(s.ThumbnailImage.Id)
            },
            Tags = s.Tags,
            Organization = s.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = s.Organization.Id,
                Name = s.Organization.Name,
                LogoImage = s.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(s.Organization.LogoImage.Id)
                }
            },
            Location = s.Location == null ? null : new LocationSimpleDto
            {
                Id = s.Location.Id,
                Region = s.Location.Region
            }
        }));
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        List<Tag> tags = (List<Tag>?)HttpContext.Items["ValidatedTags"] ?? [];

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        if (organizationId == null) return Unauthorized("You are not a part of an organization");

        var service = new Service
        {
            Title = request.Title,
            Description = request.Description,
            LocationId = request.LocationId,
            Tags = tags,
            OrganizationId = (Guid)organizationId,
            CreatedByUserId = user.Id,
        };

        await _dbContext.Services.AddAsync(service);

        await _dbContext.SaveChangesAsync();

        return StatusCode(201, service.Id);
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpDelete]
    public IActionResult DeleteService()
    {
        return Ok("Create service");
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpPatch]
    public IActionResult UpdateService()
    {
        return Ok("Create service");
    }

    [HttpGet("{ServiceId}")]
    public async Task<IActionResult> GetService([FromRoute] GetServiceRequestDto request)
    {
        var service = await _dbContext.Services
                                    .Where(s => s.Id == request.ServiceId)
                                    .Include(s => s.CreatedByUser)
                                        .ThenInclude(u => u!.ProfileImage)
                                    .Include(s => s.Gallery)
                                        .ThenInclude(g => g!.Images)
                                    .Include(s => s.ThumbnailImage)
                                    .Include(s => s.Tags)
                                    .Include(s => s.Organization)
                                    .ThenInclude(o => o!.LogoImage)
                                    .Include(s => s.Location)
                                    .FirstOrDefaultAsync();

        if (service == null) return NotFound("Service not found");

        return Ok(new GetServicesResponseDto
        {
            Id = service.Id,
            Title = service.Title,
            CreatedDateTime = service.CreatedDateTime,
            CreatedByUser = service.CreatedByUser == null ? null : new UserSimpleDto
            {
                Id = service.CreatedByUser.Id,
                Username = service.CreatedByUser.Username,
                ProfileImage = service.CreatedByUser.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(service.CreatedByUser.ProfileImage.Id)
                }
            },
            ThumbnailImage = service.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(service.ThumbnailImage.Id)
            },
            Tags = service.Tags,
            Organization = service.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = service.Organization.Id,
                Name = service.Organization.Name,
                LogoImage = service.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(service.Organization.LogoImage.Id)
                }
            },
            Location = service.Location == null ? null : new LocationSimpleDto
            {
                Id = service.Location.Id,
                Region = service.Location.Region
            }
        });
    }


}