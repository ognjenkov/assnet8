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

        }
        private async Task<bool> IsUniqueName(string name, CancellationToken token)
        {
            return !await _dbContext.Fields.AnyAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken: token);
        }
        private bool BeAValidGoogleMapsLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return false;

            var pattern = @"^(https?:\/\/)?(www\.)?(google\.com\/maps|maps\.google\.com|goo\.gl\/maps)\/.+$";
            return System.Text.RegularExpressions.Regex.IsMatch(link, pattern);
        }

        private async Task<bool> ExistLocation(Guid locationId, CancellationToken token)
        {
            return await _dbContext.Locations.AnyAsync(l => l.Id == locationId, cancellationToken: token);
        }
    }
}