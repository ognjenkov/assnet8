using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Services.Request
{
    public class CreateServiceRequestDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Guid LocationId { get; set; }
        public Guid[] TagIds { get; set; } = [];
        public required IFormFile ServiceImage { get; set; }
        public IFormFile[]? Images { get; set; }
    }
    public class CreateServiceRequestDtoValidator : AbstractValidator<CreateServiceRequestDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateServiceRequestDtoValidator(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters")
                .MaximumLength(20).WithMessage("Title must be at most 20 characters");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters")
                .MaximumLength(600).WithMessage("Title must be at most 600 characters");

            RuleFor(x => x.LocationId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("FieldId is required")
                .MustAsync(ExistLocation).WithMessage("Location does not exist");

            RuleFor(x => x.TagIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("TagIds is required")
                .MustAsync(AreTagIdsValid).WithMessage("TagIds are not valid.");

            RuleFor(x => x.ServiceImage)
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
                .Where(t => tagIds.Contains(t.Id) && t.Type == TagType.Service)
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
}