using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Games.Request;

public class CreateGameRequestDto
{
    public required string Title { get; set; }
    public required DateTime StartDateTime { get; set; }
    public string? Description { get; set; }
    public required Guid FieldId { get; set; }
    public Guid[] TagIds { get; set; } = [];
    public int MaxTotal { get; set; } = 999;
    public int MaxRent { get; set; } = 0;
    public bool OutsourceEntries { get; set; } = false;
    public string? OutsourceEntriesInstructions { get; set; } = null;
}
public class CreateGameRequestDtoValidator : AbstractValidator<CreateGameRequestDto>
{

    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CreateGameRequestDtoValidator(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this._dbContext = dbContext;
        this._httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters")
            .MaximumLength(60).WithMessage("Title must be at most 60 characters");

        RuleFor(x => x.StartDateTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("StartDateTime is required")
            .GreaterThan(DateTime.MinValue).WithMessage("Date is not valid.");

        RuleFor(x => x.FieldId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("FieldId is required")
            .MustAsync(IsValidFieldId).WithMessage("FieldId is not valid.");

        RuleFor(x => x.TagIds)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("TagIds is required")
            .MustAsync(AreTagIdsValid).WithMessage("TagIds are not valid.");

        RuleFor(x => x.MaxTotal)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("Op number cannot be negative")
            .LessThanOrEqualTo(999).WithMessage("Op number cannot be greater than 999");

        RuleFor(x => x.MaxRent)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).WithMessage("Rent number cannot be negative")
            .LessThanOrEqualTo(999).WithMessage("Rent number cannot be greater than 999");
    }
    private async Task<bool> IsValidFieldId(Guid fieldId, CancellationToken token)
    {
        return await _dbContext.Fields.AnyAsync(f => f.Id == fieldId, cancellationToken: token);
    }
    private async Task<bool> AreTagIdsValid(Guid[] tagIds, CancellationToken token)
    {
        if (tagIds.Length == 0) return true;

        var tags = await _dbContext.Tags
            .Where(t => tagIds.Contains(t.Id) && t.Type == TagType.Game)
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
}