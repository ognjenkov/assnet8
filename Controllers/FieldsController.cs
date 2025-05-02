using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Fields.Request;
using assnet8.Dtos.Fields.Response;
using assnet8.Dtos.Pagination;
using assnet8.Services.Account;
using assnet8.Services.Images;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace assnet8.Controllers;
[Route("fields")]
public class FieldsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IAccountService _accountService;
    private readonly ICloudImageService _imageService;
    private readonly IGoogleMapsService _googleMapsService;

    private readonly INextJsRevalidationService _nextJsRevalidationService;

    public FieldsController(AppDbContext dbContext, IAccountService accountService, ICloudImageService imageService, IGoogleMapsService googleMapsService, INextJsRevalidationService nextJsRevalidationService)
    {
        this._dbContext = dbContext;
        this._accountService = accountService;
        this._imageService = imageService;
        this._googleMapsService = googleMapsService;
        this._nextJsRevalidationService = nextJsRevalidationService;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<GetFieldsResponseDto>>> GetFields([FromQuery] GetFieldsRequestDto request)
    {
        var query = _dbContext.Fields
                              .Include(f => f.ThumbnailImage)
                              .Include(f => f.Location)
                              .Include(f => f.Organization)
                              .ThenInclude(o => o!.LogoImage)
                              .OrderByDescending(f => f.CreateDateTime)
                              .AsQueryable();

        // if (!string.IsNullOrWhiteSpace(request.Name))
        // {
        //     query = query.Where(f => f.Name.Contains(request.Name));
        // }

        // if (request.OrganizationId.HasValue)
        // {
        //     query = query.Where(f => f.OrganizationId == request.OrganizationId.Value);
        // }

        // if (!string.IsNullOrWhiteSpace(request.Region))
        // {
        //     query = query.Where(f => f.Location != null && f.Location.Region == request.Region);
        // }

        var totalCount = await query.CountAsync();

        var fields = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var fieldDtos = fields.Select(f => new GetFieldsResponseDto
        {
            Id = f.Id,
            Name = f.Name,
            GoogleMapsLink = f.GoogleMapsLink,
            Latitude = f.Latitude,
            Longitude = f.Longitude,
            ThumbnailImage = f.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(f.ThumbnailImage.Id)
            },
            Location = f.Location == null ? null : new LocationSimpleDto
            {
                Id = f.Location.Id,
                Region = f.Location.Region,
            },
            Organization = f.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = f.Organization.Id,
                Name = f.Organization.Name,
                LogoImage = f.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(f.Organization.LogoImage.Id)
                }
            }
        });

        return Ok(new PaginatedResponseDto<GetFieldsResponseDto>
        {
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Items = fieldDtos
        });
    }

    [HttpGet("ids")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetFieldIds()
    {
        var ids = await _dbContext.Fields
        .AsNoTracking()
        .Select(p => p.Id)
        .ToListAsync();

        return Ok(ids);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.TeamLeader, Roles.Member, Roles.OrganizationOwner, Roles.ServiceProvider)]
    [HttpGet("owned")]
    public async Task<ActionResult<IEnumerable<GetOwnedFieldsResponseDto>>> GetOwnedFields()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var guidUserId = Guid.Parse(userId!);

        var user = await _accountService.GetAccountFromUserId(guidUserId);

        var organizationId = user!.Organization?.Id ?? user!.Membership?.Team?.Organization?.Id;

        var fields = await _dbContext.Fields
                                        .Where(f => f.OrganizationId == organizationId)
                                        .Include(f => f.ThumbnailImage)
                                        .Include(f => f.Location)
                                        .ToListAsync();

        return Ok(fields.Select(f => new GetOwnedFieldsResponseDto
        {
            Id = f.Id,
            Name = f.Name,
            GoogleMapsLink = f.GoogleMapsLink,
            ThumbnailImage = f.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(f.ThumbnailImage.Id)
            },
            Location = f.Location == null ? null : new LocationSimpleDto
            {
                Id = f.Location.Id,
                Region = f.Location.Region,
            }
        }));
    }

    [HttpGet("{FieldId}")]
    public async Task<ActionResult<GetFieldResponseDto>> GetField([FromRoute] GetFieldRequestDto request)
    {
        //vratices id organizacije, na frontu ce se proveriti dal je taj id jednak sa idjem tvoje organizacije onda ce da se provere tvoji rolovi i onda ces ima dugme edit ili delete itd...
        var field = await _dbContext.Fields.Where(f => f.Id == request.FieldId)
                                    .Include(f => f.ThumbnailImage)
                                    .Include(f => f.Gallery)
                                        .ThenInclude(g => g!.Images)
                                    .Include(f => f.Gallery)
                                        .ThenInclude(g => g!.User)
                                            .ThenInclude(u => u!.ProfileImage)
                                    .Include(f => f.Games)
                                    .Include(f => f.Location)
                                    .Include(f => f.Organization)
                                    .ThenInclude(o => o!.LogoImage)
                                    .FirstOrDefaultAsync();

        if (field == null) return NotFound("Field not found");

        return Ok(new GetFieldResponseDto
        {
            Id = field.Id,
            Name = field.Name,
            GoogleMapsLink = field.GoogleMapsLink,
            Latitude = field.Latitude,
            Longitude = field.Longitude,
            ThumbnailImage = field.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(field.ThumbnailImage.Id)
            },
            Location = field.Location == null ? null : new LocationSimpleDto
            {
                Id = field.Location.Id,
                Region = field.Location.Region,
            },
            Organization = field.Organization == null ? null : new OrganizationSimpleDto
            {
                Id = field.Organization.Id,
                Name = field.Organization.Name,
                LogoImage = field.Organization.LogoImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(field.Organization.LogoImage.Id)
                }
            },
            Gallery = field.Gallery == null ? null : new GallerySimpleDto
            {
                Title = field.Gallery.Title,
                Images = field.Gallery.Images.Select(i => new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(i.Id)
                }).ToList(),
                CreateDateTime = field.Gallery.CreateDateTime,
                User = field.Gallery.User == null ? null : new UserSimpleDto
                {
                    Id = field.Gallery.User.Id,
                    Username = field.Gallery.User.Username,
                    ProfileImage = field.Gallery.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = Utils.Utils.GenerateImageFrontendLink(field.Gallery.User.ProfileImage.Id)
                    }
                }
            },
            Games = field.Games?.Select(g => new GameSimpleDto
            {
                Id = g.Id,
                Title = g.Title,
                StartDateTime = g.StartDateTime,
            }).ToList() ?? [],
        });
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPost]
    public async Task<IActionResult> CreateField([FromForm] CreateFieldRequestDto request)
    {//TODO ova metoda nije gotova jer jos nema slika :)
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(userGuid);

        if (user == null) return NotFound("User not found");

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        //ovo se jedino desava ako tim nema organizaciju, zbog autorizacije
        if (organizationId == null) return Unauthorized("Your team does not have an organization");

        var (latitute, longitude) = await _googleMapsService.GetCoordinatesFromUrlAsync(request.GoogleMapsLink);

        var field = new Field
        {
            Name = request.Name,
            GoogleMapsLink = request.GoogleMapsLink,
            LocationId = request.LocationId,
            OrganizationId = (Guid)organizationId,
            Latitude = latitute ?? 0,
            Longitude = longitude ?? 0,
        };

        await _dbContext.Fields.AddAsync(field);
        await _dbContext.SaveChangesAsync();



        try
        {
            var image = await _imageService.UploadImage(user, request.FieldImage);
            await _dbContext.Images.AddAsync(image);

            field.ThumbnailImageId = image.Id;

            await _dbContext.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Failed to upload field image");
            // throw;
        }

        if (request.Images != null && request.Images.Length > 0)
        {
            var gallery = new Gallery
            {
                Title = request.Name,
                CreateDateTime = DateTime.Now,
                UserId = user.Id,
                Field = field,
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
                    _nextJsRevalidationService.RevalidatePathAsync($"/fields/{field.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("fields"),
                    _nextJsRevalidationService.RevalidateTagAsync($"field-{field.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"field-{field.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return StatusCode(201, field.Id);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpDelete("{FieldId}")]
    public async Task<IActionResult> DeleteFieldAsync([FromRoute] DeleteFieldRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(userGuid);
        if (user == null) return NotFound("User not found");

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        var field = await _dbContext.Fields
                            .Where(f => f.Id == request.FieldId && f.OrganizationId == organizationId)
                            .Include(f => f.Gallery)
                                .ThenInclude(g => g!.Images)
                            .Include(f => f.ThumbnailImage)
                            .FirstOrDefaultAsync();

        if (field == null) return NotFound("Field not found");

        try
        {
            _dbContext.Fields.Remove(field);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Field cannot be deleted due to existing references.");
        }

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
        await _dbContext.SaveChangesAsync();

        try
        {
            await Task.WhenAll(
                _nextJsRevalidationService.RevalidatePathAsync($"/fields/{field.Id}"),
                _nextJsRevalidationService.RevalidateTagAsync("fields"),
                _nextJsRevalidationService.RevalidateTagAsync($"field-{field.Id}-simple"),
                _nextJsRevalidationService.RevalidateTagAsync($"field-{field.Id}")
            );
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Failed to revalidate");
        }


        return Ok("Field deleted successfully.");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPatch]
    public IActionResult UpdateField()
    {
        return Ok("Update field");
    }

    [HttpGet("{FieldId}/simple")]
    public async Task<ActionResult<FieldSimpleDto>> GetFieldSimple([FromRoute] GetFieldSimpleRequestDto request)
    {
        var field = await _dbContext.Fields
                            .Where(f => f.Id == request.FieldId)
                            .Include(f => f.ThumbnailImage)
                            .Include(f => f.Location)
                            .FirstOrDefaultAsync();

        if (field == null) return NotFound("Field not found");

        return Ok(new FieldSimpleDto
        {
            Id = field.Id,
            Name = field.Name,
            ThumbnailImage = field.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(field.ThumbnailImage.Id)
            },
            GoogleMapsLink = field.GoogleMapsLink,
            Location = field.Location == null ? null : new LocationSimpleDto
            {
                Id = field.Location.Id,
                Region = field.Location.Region
            }
        });
    }
}