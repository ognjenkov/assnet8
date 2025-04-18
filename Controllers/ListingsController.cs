using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using assnet8.Dtos.Listings.Request;
using assnet8.Dtos.Listings.Response;
using assnet8.Services.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[Route("listings")]
public class ListingsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IImageService _imageService;

    public ListingsController(AppDbContext dbContext, IImageService imageService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetListings()
    {
        var listings = await _dbContext.Listings
                                            .Include(l => l.ThumbnailImage)
                                            .Include(l => l.Tags)
                                            .Include(l => l.Location)
                                            .ToListAsync();

        return Ok(listings.Select(l => new GetListingsResponseDto
        {
            Id = l.Id,
            RefreshDateTime = l.RefreshDateTime,
            Type = l.Type,
            Condition = l.Condition,
            Status = l.Status,
            Title = l.Title,
            Description = l.Description,
            Price = l.Price,
            ThumbnailImage = l.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(l.ThumbnailImage.Id)
            },
            Tags = l.Tags,
            Location = l.Location == null ? null : new LocationSimpleDto
            {
                Id = l.Location.Id,
                Region = l.Location.Region
            }
        }));
    }

    [HttpGet("{ListingId}")]
    public async Task<IActionResult> GetListing([FromRoute] GetListingRequestDto request)
    {
        var listing = await _dbContext.Listings
                                    .Where(l => l.Id == request.ListingId)
                                    .Include(l => l.ThumbnailImage)
                                    .Include(l => l.Tags)
                                    .Include(l => l.Location)
                                    .Include(l => l.User)
                                        .ThenInclude(u => u!.ProfileImage)
                                    .Include(l => l.Gallery)
                                        .ThenInclude(g => g!.Images)
                                    .FirstOrDefaultAsync();

        if (listing == null) return NotFound("Listing not found");

        return Ok(new GetListingResponseDto
        {
            Id = listing.Id,
            CreateDateTime = listing.CreateDateTime,
            RefreshDateTime = listing.RefreshDateTime,
            Type = listing.Type,
            Condition = listing.Condition,
            Status = listing.Status,
            Title = listing.Title,
            Description = listing.Description,
            Price = listing.Price,
            ContactInfo = listing.ContactInfo,
            User = new UserSimpleDto
            {
                Id = listing.User!.Id,
                Username = listing.User.Username,
                ProfileImage = listing.User.ProfileImage == null ? null : new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(listing.User.ProfileImage.Id)
                }
            },
            ThumbnailImage = listing.ThumbnailImage == null ? null : new ImageSimpleDto
            {
                Url = Utils.Utils.GenerateImageFrontendLink(listing.ThumbnailImage.Id)
            },
            Tags = listing.Tags,
            Gallery = listing.Gallery == null ? null : new GallerySimpleDto
            {
                Title = listing.Gallery.Title,
                CreateDateTime = listing.Gallery.CreateDateTime,
                Images = listing.Gallery.Images.Select(i => new ImageSimpleDto
                {
                    Url = Utils.Utils.GenerateImageFrontendLink(i.Id)
                }).ToList() ?? [],
            },
            Location = listing.Location == null ? null : new LocationSimpleDto
            {
                Id = listing.Location.Id,
                Region = listing.Location.Region
            }
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateListing([FromForm] CreateListingRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        if (userId != Guid.Parse(userId).ToString()) return Unauthorized();

        var user = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

        if (user == null) return NotFound("User not found");

        List<Tag> tags = (List<Tag>?)HttpContext.Items["ValidatedTags"] ?? [];

        var listing = new Listing
        {
            Type = request.Type,
            Condition = request.Condition,
            Status = ListingStatus.Active,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            ContactInfo = request.ContactInfo,
            User = user,
            UserId = user.Id,
            LocationId = request.LocationId,
            Tags = tags,
        };
        await _dbContext.Listings.AddAsync(listing);
        await _dbContext.SaveChangesAsync();

        try
        {
            var image = await _imageService.UploadImage(user, request.ThumbnailImage);
            await _dbContext.Images.AddAsync(image);

            listing.ThumbnailImageId = image.Id;

            await _dbContext.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Failed to upload profile image");
            // throw;
        }

        if (request.Images != null && request.Images.Length > 0)
        {
            var gallery = new Gallery
            {
                Title = request.Title,
                CreateDateTime = DateTime.Now,
                UserId = user.Id,
                Listing = listing,
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
                    System.Console.WriteLine("Failed to upload profile image");
                    // throw;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        return StatusCode(201, listing.Id);
    }

    [Authorize]
    [HttpDelete]
    public IActionResult DeleteListing()
    {
        return Ok("Delete listing");
    }

    [Authorize]
    [HttpPatch]
    public IActionResult UpdateListing()
    {
        return Ok("Update listing");
    }

}