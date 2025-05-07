using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class InviteUserToTeamRequestDto
{
    public Guid UserId { get; set; }
}
public class InviteUserToTeamRequestDtoValidator : AbstractValidator<InviteUserToTeamRequestDto>
{
    public InviteUserToTeamRequestDtoValidator()
    {
        RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId is required");
    }
}