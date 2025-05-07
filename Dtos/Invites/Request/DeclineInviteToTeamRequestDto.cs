using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class DeclineInviteToTeamRequestDto
{
    public Guid InviteId { get; set; }
}
public class DeclineInviteToTeamRequestDtoValidator : AbstractValidator<DeclineInviteToTeamRequestDto>
{
    public DeclineInviteToTeamRequestDtoValidator()
    {
        RuleFor(x => x.InviteId)
             .NotEmpty().WithMessage("InviteId is required");

    }
}