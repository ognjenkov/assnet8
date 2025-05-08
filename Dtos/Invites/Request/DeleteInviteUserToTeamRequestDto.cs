using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class DeleteInviteUserToTeamRequestDto
{
    public Guid InviteId { get; set; }
}
public class DeleteInviteUserToTeamRequestDtoValidator : AbstractValidator<DeleteInviteUserToTeamRequestDto>
{
    public DeleteInviteUserToTeamRequestDtoValidator()
    {
        RuleFor(x => x.InviteId)
             .NotEmpty().WithMessage("InviteId is required");

    }
}