using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Organizations.Request;
using assnet8.Dtos.Organizations.Response;
using assnet8.Services.Images;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;

[Authorize]
[Route("organizations")]
public class OrganizationsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly ICloudImageService _imageService;

    private readonly INextJsRevalidationService _nextJsRevalidationService;

    public OrganizationsController(AppDbContext dbContext, ICloudImageService imageService, INextJsRevalidationService nextJsRevalidationService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;

        this._nextJsRevalidationService = nextJsRevalidationService;
    }

    [VerifyRoles(Roles.Creator, Roles.OrganizationOwner, Roles.Organizer, Roles.ServiceProvider)]
    [HttpPatch]
    public IActionResult UpdateOrganization()
    {
        // try
        // {
        //     await Task.WhenAll(
        //             _nextJsRevalidationService.RevalidatePathAsync($"/organizations/{organization.Id}"),
        //             _nextJsRevalidationService.RevalidateTagAsync("organizations"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}-simple"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}")
        //         );
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex);
        // }

        return Ok("Update organization");
    }

    [VerifyRoles(Roles.Creator, Roles.OrganizationOwner)]
    [HttpDelete]
    public async Task<IActionResult> DeleteOrganization()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !Guid.TryParse(userId, out var userGuid)) return Unauthorized();
        var teamId = User.FindFirst("TeamId")?.Value;
        Guid.TryParse(teamId, out var teamGuid);

        var organization = await _dbContext.Organizations
                                .AsSplitQuery()
                                .Include(o => o.Fields)
                                    .ThenInclude(f => f.ThumbnailImage)
                                .Include(o => o.Fields)
                                    .ThenInclude(f => f.Gallery)
                                        .ThenInclude(g => g!.Images)
                                .Include(o => o.Games)
                                .Include(o => o.LogoImage)
                                .Include(o => o.Services)
                                    .ThenInclude(s => s.ThumbnailImage)
                                .Include(o => o.Services)
                                    .ThenInclude(s => s.Gallery)
                                        .ThenInclude(g => g!.Images)
                                .FirstOrDefaultAsync(o => o.UserId == userGuid || o.TeamId == teamGuid);
        if (organization == null) return NotFound("Organization not found");

        _dbContext.Games.RemoveRange(organization.Games);
        try
        {
            await _nextJsRevalidationService.RevalidateTagAsync("games"); //TODO mozda i za pojedinacan game, ali ne mora vrv, osim ako ima neki field report koji referencira game
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Failed to revalidate");
        }

        _dbContext.Fields.RemoveRange(organization.Fields);
        await _dbContext.SaveChangesAsync();
        foreach (var field in organization.Fields)
        {
            if (field.ThumbnailImage != null)
            {
                await _imageService.DeleteImage(field.ThumbnailImage);
            }

            if (field.Gallery != null)
            {
                foreach (var image in field.Gallery.Images)
                {
                    await _imageService.DeleteImage(image);
                }

                _dbContext.Galleries.Remove(field.Gallery);
            }
            try
            {
                await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/fields/{field.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync($"field-{field.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"field-{field.Id}")
                );
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Failed to revalidate");
            }
        }
        await _dbContext.SaveChangesAsync();
        await _nextJsRevalidationService.RevalidateTagAsync("fields");

        _dbContext.Services.RemoveRange(organization.Services);
        await _dbContext.SaveChangesAsync();
        foreach (var service in organization.Services)
        {
            if (service.ThumbnailImage != null)
            {
                await _imageService.DeleteImage(service.ThumbnailImage);
            }

            if (service.Gallery != null)
            {
                foreach (var image in service.Gallery.Images)
                {
                    await _imageService.DeleteImage(image);
                }

                _dbContext.Galleries.Remove(service.Gallery);
            }
            try
            {
                await Task.WhenAll(
                        _nextJsRevalidationService.RevalidatePathAsync($"/services/{service.Id}"),
                        _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}-simple"),
                        _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}")
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        await _dbContext.SaveChangesAsync();
        await _nextJsRevalidationService.RevalidateTagAsync("services");



        try
        {
            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Organization cannot be deleted due to existing references.");
        }

        if (organization.LogoImage != null)
        {
            await _imageService.DeleteImage(organization.LogoImage);
        }
        await _dbContext.SaveChangesAsync();

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/organizations/{organization.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("organizations"),
                    _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return Ok("Delete organization");
    }

    [AllowAnonymous]
    [HttpGet("{OrganizationId}/simple")]
    public async Task<ActionResult<OrganizationSimpleDto>> GetOrganizationSimple([FromRoute] GetOrganizationSimpleRequestDto request)
    {
        var organization = await _dbContext.Organizations
                            .Where(o => o.Id == request.OrganizationId)
                            .Include(o => o.LogoImage)
                            .FirstOrDefaultAsync();

        if (organization == null) return NotFound("Organization not found");

        return Ok(new OrganizationSimpleDto
        {
            Id = organization.Id,
            Name = organization.Name,
            LogoImage = organization.LogoImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(organization.LogoImage.Id)
            }
        });
    }

    [AllowAnonymous]
    [HttpGet("{OrganizationId}/card")]
    public async Task<ActionResult<GetOrganizationCardResponseDto>> GetOrganizationCard([FromRoute] GetOrganizationCardRequestDto request)
    {
        var organization = await _dbContext.Organizations
                            .Where(o => o.Id == request.OrganizationId)
                            .AsSplitQuery()
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
            CreateDateTime = new DateTimeOffset(organization.CreateDateTime, TimeSpan.Zero),
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
                StartDateTime = new DateTimeOffset(g.StartDateTime, TimeSpan.Zero)
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

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/organizations/{organization.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("organizations"),
                    _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}")// TODO mozda revalidacije individualnog tima ovde?
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

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

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/organizations/{organization.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("organizations"),
                    _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"organization-{organization.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return StatusCode(201, organization.Id);
    }

}