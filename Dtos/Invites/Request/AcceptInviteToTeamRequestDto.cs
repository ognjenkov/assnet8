using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class AcceptInviteToTeamRequestDto
{
    public Guid InviteId { get; set; }
}
public class AcceptInviteToTeamRequestDtoValidator : AbstractValidator<AcceptInviteToTeamRequestDto>
{
    public AcceptInviteToTeamRequestDtoValidator()
    {
        RuleFor(x => x.InviteId)
             .NotEmpty().WithMessage("InviteId is required");
    }
}