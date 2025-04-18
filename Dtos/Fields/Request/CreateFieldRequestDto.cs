using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Fields.Request
{
    public class CreateFieldRequestDto
    {
        public required string Name { get; set; }
        public required string GoogleMapsLink { get; set; }
        public required Guid LocationId { get; set; }
        public required IFormFile FieldImage { get; set; }
        public IFormFile[]? Images { get; set; }
    }
    public class CreateFieldRequestDtoValidator : AbstractValidator<CreateFieldRequestDto>
    {
        private readonly AppDbContext _dbContext;
        public CreateFieldRequestDtoValidator(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters")
                .MaximumLength(20).WithMessage("Name must be at most 20 characters")
                .MustAsync(IsUniqueName).WithMessage("Name is already taken");

            RuleFor(x => x.GoogleMapsLink)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Google maps link is required")
                .Must(BeAValidGoogleMapsLink).WithMessage("Google maps link is not valid");

            RuleFor(x => x.LocationId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("FieldId is required")
                .MustAsync(ExistLocation).WithMessage("Location does not exist");

            RuleFor(x => x.FieldImage)
                .Cascade(CascadeMode.Stop)
                .Must(file => file.Length > 0).WithMessage("Image file cannot be empty")
                .Must(file => file.Length <= 5 * 1024 * 1024).WithMessage("Image must be less than 5MB")
                .Must(file => new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.FileName).ToLower())).WithMessage("Only JPG and PNG images are allowed");

            RuleFor(x => x.Images)
                .Must(AreImagesValid)
                .WithMessage("Images are not valid");

        }
        private async Task<bool> IsUniqueName(string name, CancellationToken token)
        {
            return !await _dbContext.Fields.AnyAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken: token);
        }
        private bool BeAValidGoogleMapsLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return false;

            var pattern = @"^https?:\/\/(?:www\.)?(?:google\.[a-z.]{2,}|goo\.gl|maps\.app\.goo\.gl)\/(?:maps(?:\/[^\s]*)?|[^\s]*)$";
            return System.Text.RegularExpressions.Regex.IsMatch(link, pattern);
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