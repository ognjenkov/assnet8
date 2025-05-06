using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Memberships.Request;

public class GetTeamMembershipSimpleRequestDto
{
    public Guid TeamId { get; set; }
    public Guid MembershipId { get; set; }
}
public class GetTeamMembershipSimpleRequestDtoValidator : AbstractValidator<GetTeamMembershipSimpleRequestDto>
{
    public GetTeamMembershipSimpleRequestDtoValidator()
    {
        RuleFor(x => x.TeamId)
             .NotEmpty().WithMessage("TeamId is required");

        RuleFor(x => x.MembershipId)
             .NotEmpty().WithMessage("MembershipId is required");
    }
}