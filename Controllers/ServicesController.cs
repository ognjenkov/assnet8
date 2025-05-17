using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Pagination;
using assnet8.Dtos.Services.Request;
using assnet8.Dtos.Services.Response;
using assnet8.Services.Account;
using assnet8.Services.Images;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;

[Route("services")]
public class ServicesController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IAccountService _accountService;
    private readonly ICloudImageService _imageService;
    private readonly INextJsRevalidationService _nextJsRevalidationService;
    public ServicesController(AppDbContext dbContext, ICloudImageService imageService, IAccountService accountService, INextJsRevalidationService nextJsRevalidationService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
        this._accountService = accountService;
        this._nextJsRevalidationService = nextJsRevalidationService;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<GetServicesResponseDto>>> GetServices([FromQuery] GetServicesRequestDto request)
    {
        var query = _dbContext.Services
                                            .AsSplitQuery()
                                            .Include(s => s.ThumbnailImage)
                                            .Include(s => s.CreatedByUser)
                                            .ThenInclude(u => u!.ProfileImage)
                                            .Include(s => s.Tags)
                                            .Include(s => s.Organization)
                                            .ThenInclude(o => o!.LogoImage)
                                            .Include(s => s.Location)
                                            .OrderByDescending(s => s.CreatedDateTime)
                                            .AsQueryable();

        if (request.LocationIds != null && request.LocationIds.Length > 0)
            query = query.Where(s => s.LocationId.HasValue && request.LocationIds.Contains(s.LocationId.Value));

        if (request.TagIds != null && request.TagIds.Length > 0)
            query = query.Where(s => s.Tags!.Any(t => request.TagIds.Contains(t.Id)));

        var totalCount = await query.CountAsync();

        var services = await query
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync();

        var serviceDtos = services.Select(s => new GetServicesResponseDto
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
                    Url = Utils.Utils.GenerateImageFrontendLink(s.CreatedByUser.ProfileImage.Id)
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
        });


        return Ok(new PaginatedResponseDto<GetServicesResponseDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Items = serviceDtos
        });
    }

    [HttpGet("ids")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetServiceIds()
    {
        var ids = await _dbContext.Services
        .AsNoTracking()
        .Select(p => p.Id)
        .ToListAsync();

        return Ok(ids);
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpPost]
    public async Task<IActionResult> CreateService([FromForm] CreateServiceRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        //ovo se jedino desava ako tim nema organizaciju, zbog autorizacije
        if (organizationId == null) return Unauthorized("Your team does not have an organization");

        List<Tag> tags = (List<Tag>?)HttpContext.Items["ValidatedTags"] ?? [];

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

        try
        {
            var image = await _imageService.UploadImage(user, request.ServiceImage);
            await _dbContext.Images.AddAsync(image);

            service.ThumbnailImageId = image.Id;

            await _dbContext.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Failed to upload service image");
            // throw;
        }

        if (request.Images != null && request.Images.Length > 0)
        {
            var gallery = new Gallery
            {
                Title = request.Title,
                CreateDateTime = DateTime.Now,
                UserId = user.Id,
                Service = service,
            };
            await _dbContext.Galleries.AddAsync(gallery);
            await _dbContext.SaveChangesAsync();

            // cudan malo redosled, nisam 
            // prosledio galleryId za slike jer galerija nije jos sacuvana,
            // dodacu slike u galeriju pa cu se naterati da ce se automacki povezati
            foreach (var image in request.Images)
            {
                try
                {
                    var img = await _imageService.UploadImage(user, image);
                    await _dbContext.Images.AddAsync(img);
                    gallery.Images.Add(img);
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed to upload gallery image");
                    // throw;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/services/{service.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("services"),
                    _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}") // da li moze da revalidira nesto sto ne postoji? mozda trebam da revalidiram i odakle se zove ovo govno
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return StatusCode(201, service.Id);
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpDelete]
    public async Task<IActionResult> DeleteServiceAsync([FromRoute] DeleteServiceRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(userGuid);

        if (user == null) return NotFound("User not found");

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        var service = await _dbContext.Services
                            .Where(f => f.Id == request.ServiceId && f.OrganizationId == organizationId)
                            .AsSplitQuery()
                            .Include(f => f.Gallery)
                                .ThenInclude(g => g!.Images)
                            .Include(f => f.ThumbnailImage)
                            .FirstOrDefaultAsync();

        if (service == null) return NotFound("Service not found");

        if (service.ThumbnailImage != null)
        {
            await _imageService.DeleteImage(service.ThumbnailImage);
        }

        if (service.Gallery != null)
        {
            var images = service.Gallery.Images.ToList();
            foreach (var image in images)
            {
                await _imageService.DeleteImage(image);
            }

            _dbContext.Galleries.Remove(service.Gallery);
        }

        _dbContext.Services.Remove(service);
        await _dbContext.SaveChangesAsync();

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/services/{service.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("services"),
                    _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return Ok("Delete service");
    }

    [Authorize]
    [VerifyRoles(Roles.ServiceProvider, Roles.OrganizationOwner, Roles.Creator)]
    [HttpPatch]
    public IActionResult UpdateService()
    {
        // try
        // {
        //     await Task.WhenAll(
        //             _nextJsRevalidationService.RevalidatePathAsync($"/services/{service.Id}"),
        //             _nextJsRevalidationService.RevalidateTagAsync("services"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}-simple"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"service-{service.Id}") 
        //         );
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex);
        // }
        return Ok("Create service");
    }

    [HttpGet("{ServiceId}")]
    public async Task<ActionResult<GetServiceResponseDto>> GetService([FromRoute] GetServiceRequestDto request)
    {
        var service = await _dbContext.Services
                                    .Where(s => s.Id == request.ServiceId)
                                    .AsSplitQuery()
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

        return Ok(new GetServiceResponseDto
        {
            Id = service.Id,
            Title = service.Title,
            Description = service.Description,
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
            Gallery = service.Gallery == null ? null : new GallerySimpleDto
            {
                Title = service.Gallery.Title,
                Images = service.Gallery.Images.Select(i => new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(i.Id)
                }).ToList(),
                CreateDateTime = service.Gallery.CreateDateTime,
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