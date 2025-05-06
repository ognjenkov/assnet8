using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Memberships.Request;

public class RemoveUserFromTeamRequestDto
{
    public required Guid UserId { get; set; }
}
public class RemoveUserFromTeamRequestDtoValidator : AbstractValidator<RemoveUserFromTeamRequestDto>
{
    public RemoveUserFromTeamRequestDtoValidator()
    {
        RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId is required");
    }
}