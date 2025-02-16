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
    }
}