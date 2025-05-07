using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Memberships.Request;

public class UpdateUserRolesRequestDto
{
    public required Guid UserId { get; set; }
    public required Guid[] RoleIds { get; set; }
}
public class UpdateUserRolesRequestDtoValidator : AbstractValidator<UpdateUserRolesRequestDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateUserRolesRequestDtoValidator(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this._dbContext = dbContext;
        this._httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.RoleIds)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Roles is required")
            .MustAsync(AreRolesValid).WithMessage("Roles are not valid.");
    }
    private async Task<bool> AreRolesValid(Guid[] roleIds, CancellationToken token)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var memberRole = await _dbContext.Roles.Where(r => r.Name == Roles.Member).FirstOrDefaultAsync(cancellationToken: token);

        if (roleIds.Length == 0)
        {
            if (httpContext != null)
            {
                httpContext.Items["ValidatedRoles"] = new List<Role> { memberRole! };
            }
            return true;
        }

        var roles = await _dbContext.Roles
            .Where(t => roleIds.Contains(t.Id))
            .ToListAsync(cancellationToken: token);

        if (roles.Count != roleIds.Length)
        {
            return false;
        }

        if (!roles.Contains(memberRole!))
        {
            roles.Add(memberRole!);
        }

        if (roles.Any(r => r.Name == Roles.Creator || r.Name == Roles.OrganizationOwner))
        {
            return false;
        }

        if (httpContext != null)
        {
            httpContext.Items["ValidatedRoles"] = roles;
        }

        return true;
    }
}