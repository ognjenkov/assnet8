using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Organizations.Request;
public class CreateOrganizationRequestDto
{
    public required string Name { get; set; }
    public IFormFile? OrganizationImage { get; set; }
}
public class CreateOrganizationRequestDtoValidator : AbstractValidator<CreateOrganizationRequestDto>
{
    private readonly AppDbContext _dbContext;
    public CreateOrganizationRequestDtoValidator(AppDbContext dbContext)
    {
        this._dbContext = dbContext;

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters")
            .MaximumLength(20).WithMessage("Name must be at most 20 characters")
            .MustAsync(IsUniqueName).WithMessage("Name is already taken");

        RuleFor(x => x.OrganizationImage)
            .Cascade(CascadeMode.Stop)
            .Must(file => file == null || file.Length > 0).WithMessage("Image file cannot be empty")
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024).WithMessage("Image must be less than 5MB")
            .Must(file => file == null || new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.FileName).ToLower())).WithMessage("Only JPG and PNG images are allowed");
    }
    private async Task<bool> IsUniqueName(string name, CancellationToken token)
    {
        return !await _dbContext.Organizations.AnyAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken: token);
    }
}