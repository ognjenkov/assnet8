using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using assnet8.Dtos.Listings.Request;
using assnet8.Dtos.Listings.Response;
using assnet8.Dtos.Pagination;
using assnet8.Services.Images;
using assnet8.Services.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace assnet8.Controllers;
[Route("listings")]
public class ListingsController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly ICloudImageService _imageService;
    private readonly INextJsRevalidationService _nextJsRevalidationService;

    public ListingsController(AppDbContext dbContext, ICloudImageService imageService, INextJsRevalidationService nextJsRevalidationService)
    {
        this._dbContext = dbContext;
        this._imageService = imageService;
        this._nextJsRevalidationService = nextJsRevalidationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetListingsResponseDto>>> GetListings([FromQuery] GetListingsRequestDto request)
    {
        var query = _dbContext.Listings
                                            .AsSplitQuery()
                                            .Include(l => l.ThumbnailImage)
                                            .Include(l => l.Tags)
                                            .Include(l => l.Location)
                                            .OrderByDescending(l => l.RefreshDateTime)
                                            .AsQueryable();

        if (request.LocationIds != null && request.LocationIds.Length > 0)
            query = query.Where(l => l.LocationId.HasValue && request.LocationIds.Contains(l.LocationId.Value));

        if (request.TagIds != null && request.TagIds.Length > 0)
            query = query.Where(l => l.Tags!.Any(t => request.TagIds.Contains(t.Id)));

        if (request.Type != null) query = query.Where(l => l.Type == request.Type);

        if (request.Conditions != null && request.Conditions.Length > 0)
            query = query.Where(l => l.Condition.HasValue && request.Conditions.Contains(l.Condition.Value));

        // if(request.Search != null) query = query.Where(l => l.Title.Contains(request.Search));

        var totalCount = await query.CountAsync();

        var listings = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var listingDtos = listings.Select(l => new GetListingsResponseDto
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
        });

        return Ok(new PaginatedResponseDto<GetListingsResponseDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Items = listingDtos
        });
    }

    [HttpGet("ids")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetListingIds()
    {
        var ids = await _dbContext.Listings
        .AsNoTracking()
        .Select(p => p.Id)
        .ToListAsync();

        return Ok(ids);
    }

    [HttpGet("{ListingId}")]
    public async Task<ActionResult<GetListingResponseDto>> GetListing([FromRoute] GetListingRequestDto request)
    {
        var listing = await _dbContext.Listings
                                    .Where(l => l.Id == request.ListingId)
                                    .AsSplitQuery()
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
    { // TODO buying nisam uradio
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var user = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.Id == userGuid);

        if (user == null) return Unauthorized("User not found");


        var listing = new Listing
        {
            Type = request.Type,
            Status = ListingStatus.Active,
            Title = request.Title,
            Description = request.Description,
            ContactInfo = request.ContactInfo,
            User = user,
            UserId = user.Id,
            LocationId = request.LocationId,
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

        if (request.Type == ListingType.Selling)
        {
            List<Tag> tags = (List<Tag>?)HttpContext.Items["ValidatedTags"] ?? [];

            listing.Condition = request.Condition;
            listing.Price = request.Price;
            listing.Tags = tags;

            await _dbContext.SaveChangesAsync();

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
        }



        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/market/{listing.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("listings"),
                    _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


        return StatusCode(201, listing.Id);
    }

    [Authorize]
    [HttpDelete("{ListingId}")]
    public async Task<IActionResult> DeleteListing([FromRoute] DeleteListingRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        if (!Guid.TryParse(userId, out var userGuid)) return Unauthorized();

        var listing = await _dbContext.Listings
                                .Where(l => l.Id == request.ListingId && l.UserId == userGuid)
                                .AsSplitQuery()
                                .Include(l => l.ThumbnailImage)
                                .Include(l => l.Gallery)
                                    .ThenInclude(g => g!.Images)
                                .FirstOrDefaultAsync();

        if (listing == null) return NotFound("listing not found"); // TODO nije bas not found nego nije taj id za tog korisnika mogce je da neki levi korisnik brrise i dobice not found ali treba unathorized ili nesto tako...

        try
        {
            _dbContext.Listings.Remove(listing);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Listing cannot be deleted due to existing references.");
        }


        if (listing.ThumbnailImage != null)
        {
            await _imageService.DeleteImage(listing.ThumbnailImage);
        }


        if (listing.Gallery != null)
        {
            var images = listing.Gallery.Images.ToList();
            foreach (var image in images)
            {
                await _imageService.DeleteImage(image);
            }
            _dbContext.Galleries.Remove(listing.Gallery);
        }

        await _dbContext.SaveChangesAsync();

        try
        {
            await Task.WhenAll(
                    _nextJsRevalidationService.RevalidatePathAsync($"/market/{listing.Id}"),
                    _nextJsRevalidationService.RevalidateTagAsync("listings"),
                    _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}-simple"),
                    _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}")
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return Ok();
    }

    [Authorize]
    [HttpPatch]
    public IActionResult UpdateListing([FromForm] CreateListingRequestDto request)
    {
        // try
        // {
        //     await Task.WhenAll(
        //             _nextJsRevalidationService.RevalidatePathAsync($"/market/{listing.Id}"),
        //             _nextJsRevalidationService.RevalidateTagAsync("listings"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}-simple"),
        //             _nextJsRevalidationService.RevalidateTagAsync($"listing-{listing.Id}")
        //         );
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex);
        // }

        return Ok("Update listing");
    }

}