using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Services.Request;
using assnet8.Dtos.Services.Response;
using assnet8.Services.Account;
using assnet8.Services.Images;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("services")]
public class ServicesController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IAccountService _accountService;
    private readonly ICloudImageService _imageService;
    public ServicesController(AppDbContext dbContext, ICloudImageService imageService, IAccountService accountService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
        this._accountService = accountService;
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