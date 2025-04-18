using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Fields.Request;
using assnet8.Dtos.Fields.Response;
using assnet8.Services.Account;
using assnet8.Services.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("fields")]
public class FieldsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IAccountService _accountService;
    private readonly IImageService _imageService;

    public FieldsController(AppDbContext dbContext, IAccountService accountService, IImageService imageService)
    {
        this._dbContext = dbContext;
        this._accountService = accountService;
        this._imageService = imageService;
    }
    [HttpGet]
    public async Task<IActionResult> GetFields()
    {
        var fields = await _dbContext.Fields
                                        .Include(f => f.ThumbnailImage)
                                        .Include(f => f.Location)
                                        .ThenInclude(l => l!.Municipalities)
                                        .Include(f => f.Organization)
                                        .ThenInclude(o => o!.LogoImage)
                                        .ToListAsync();
        return Ok(fields.Select(f => new GetFieldsResponseDto
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
        }));
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.TeamLeader, Roles.Member, Roles.OrganizationOwner, Roles.ServiceProvider)]
    [HttpGet("owned")]
    public IActionResult GetOwnedFields()
    {
        return Ok("Get owned fields");
    }

    [HttpGet("{FieldId}")]
    public async Task<IActionResult> GetField([FromRoute] GetFieldRequestDto request)
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
        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _accountService.GetAccountFromUserId(Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        var organizationId = user.Organization?.Id ?? user.Membership?.Team?.Organization?.Id;

        //ovo se jedino desava ako tim nema organizaciju, zbog autorizacije
        if (organizationId == null) return Unauthorized("Your team does not have an organization");

        var field = new Field
        {
            Name = request.Name,
            GoogleMapsLink = request.GoogleMapsLink,
            LocationId = request.LocationId,
            OrganizationId = (Guid)organizationId
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

        return StatusCode(201, field.Id);
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpDelete]
    public IActionResult DeleteField()
    {
        return Ok("Delete field");
    }

    [Authorize]
    [VerifyRoles(Roles.Creator, Roles.Organizer, Roles.OrganizationOwner)]
    [HttpPatch]
    public IActionResult UpdateField()
    {
        return Ok("Update field");
    }

}