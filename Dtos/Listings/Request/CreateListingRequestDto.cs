using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Listings.Request;

public class CreateListingRequestDto
{
    public ListingType Type { get; set; }
    public ListingCondition Condition { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Price { get; set; }
    public required string ContactInfo { get; set; }
    public Guid LocationId { get; set; }
    public Guid[] TagIds { get; set; } = [];
    public required IFormFile ThumbnailImage { get; set; }
    public IFormFile[]? Images { get; set; }
}
public class CreateListingRequestDtoValidator : AbstractValidator<CreateListingRequestDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CreateListingRequestDtoValidator(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this._dbContext = dbContext;
        this._httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters")
            .MaximumLength(60).WithMessage("Title must be at most 60 characters");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters")
            .MaximumLength(600).WithMessage("Title must be at most 600 characters");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Price is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters")
            .MaximumLength(60).WithMessage("Title must be at most 60 characters");

        RuleFor(x => x.ContactInfo)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("ContactInfo is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters")
            .MaximumLength(60).WithMessage("Title must be at most 60 characters");

        RuleFor(x => x.LocationId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("LocationId is required")
            .MustAsync(ExistLocation).WithMessage("Location does not exist");

        RuleFor(x => x.TagIds)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("TagIds is required")
            .MustAsync(AreTagIdsValid).WithMessage("TagIds are not valid.");

        RuleFor(x => x.ThumbnailImage)
            .Cascade(CascadeMode.Stop)
            .Must(file => file.Length > 0).WithMessage("Image file cannot be empty")
            .Must(file => file.Length <= 5 * 1024 * 1024).WithMessage("Image must be less than 5MB")
            .Must(file => new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.FileName).ToLower())).WithMessage("Only JPG and PNG images are allowed");

        RuleFor(x => x.Images)
            .Must(AreImagesValid)
            .WithMessage("Images are not valid");
    }
    private async Task<bool> AreTagIdsValid(Guid[] tagIds, CancellationToken token)
    {
        if (tagIds.Length == 0) return true;

        var tags = await _dbContext.Tags
            .Where(t => tagIds.Contains(t.Id) && t.Type == TagType.Listing)
            .ToListAsync(cancellationToken: token);

        if (tags.Count != tagIds.Length)
        {
            return false;
        }

        // Store the validated tags in HttpContext.Items
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            httpContext.Items["ValidatedTags"] = tags;
        }

        return true;
    }
    private async Task<bool> ExistLocation(Guid locationId, CancellationToken token)
    {
        return await _dbContext.Locations.AnyAsync(l => l.Id == locationId, cancellationToken: token);
    }

    private bool AreImagesValid(IFormFile[]? images)
    {
        if (images == null) return true;
        if (images.Length == 0) return true;
        if (images.Length > 8) return false;

        return images.All(IsImageValid);
    }

    private bool IsImageValid(IFormFile image)
    {
        if (image == null) return false;

        return image.Length <= 5 * 1024 * 1024 && new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(image.FileName).ToLower());
    }
}