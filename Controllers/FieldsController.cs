using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Fields.Request;
using assnet8.Dtos.Fields.Response;
using assnet8.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
public class FieldsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IAccountService _accountService;

    public FieldsController(AppDbContext dbContext, IAccountService accountService)
    {
        this._dbContext = dbContext;
        this._accountService = accountService;
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
            ThumbnailImage = f.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = f.ThumbnailImage.Url
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
                    Url = f.Organization.LogoImage.Url
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

    [HttpGet("{fieldId}")]
    public async Task<IActionResult> GetField([FromQuery] GetFieldRequestDto request)
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
            ThumbnailImage = field.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = field.ThumbnailImage.Url
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
                    Url = field.Organization.LogoImage.Url
                }
            },
            Gallery = field.Gallery == null ? null : new GallerySimpleDto
            {
                Title = field.Gallery.Title,
                Images = field.Gallery.Images.Select(i => new ImageSimpleDto
                {
                    Url = i.Url
                }).ToList(),
                CreateDateTime = field.Gallery.CreateDateTime,
                User = field.Gallery.User == null ? null : new UserSimpleDto
                {
                    Id = field.Gallery.User.Id,
                    Username = field.Gallery.User.Username,
                    ProfileImage = field.Gallery.User.ProfileImage == null ? null : new ImageSimpleDto
                    {
                        Url = field.Gallery.User.ProfileImage.Url
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
    public async Task<IActionResult> CreateField([FromBody] CreateFieldRequestDto request)
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