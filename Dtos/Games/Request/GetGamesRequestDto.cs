using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using assnet8.Dtos.Pagination;

using FluentValidation;

namespace assnet8.Dtos.Games.Request;

public class GetGamesRequestDto : PaginationQueryDto
{
    public Guid[]? LocationIds { get; set; }
    public Guid[]? TagIds { get; set; }
}
public class GetGamesRequestDtoValidator : AbstractValidator<GetGamesRequestDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetGamesRequestDtoValidator(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this._dbContext = dbContext;
        this._httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.LocationIds)
            .Cascade(CascadeMode.Stop)
            .MustAsync(AreLocationIdsValid).WithMessage("LocationIds are not valid.");

        RuleFor(x => x.TagIds)
            .Cascade(CascadeMode.Stop)
            .MustAsync(AreTagIdsValid).WithMessage("TagIds are not valid.");

    }
    private async Task<bool> AreTagIdsValid(Guid[]? tagIds, CancellationToken token)
    {
        if (tagIds == null) return true;
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
    private async Task<bool> AreLocationIdsValid(Guid[]? locationIds, CancellationToken token)
    {
        if (locationIds == null) return true;
        if (locationIds.Length == 0) return true;

        var locations = await _dbContext.Locations
            .Where(l => locationIds.Contains(l.Id))
            .ToListAsync(cancellationToken: token);

        if (locations.Count != locationIds.Length)
        {
            return false;
        }

        // Store the validated locations in HttpContext.Items
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            httpContext.Items["ValidatedLocations"] = locations;
        }

        return true;
    }
}