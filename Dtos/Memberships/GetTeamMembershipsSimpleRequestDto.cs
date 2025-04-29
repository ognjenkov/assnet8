using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Memberships;

public class GetTeamMembershipsSimpleRequestDto
{
    public Guid TeamId { get; set; }
}
public class GetTeamMembershipsSimpleRequestDtoValidator : AbstractValidator<GetTeamMembershipsSimpleRequestDto>
{
    public GetTeamMembershipsSimpleRequestDtoValidator()
    {
        RuleFor(x => x.TeamId)
             .NotEmpty().WithMessage("TeamId is required");
    }
}