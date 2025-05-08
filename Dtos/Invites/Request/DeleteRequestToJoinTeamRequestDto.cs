using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class DeleteRequestToJoinTeamRequestDto
{
    public Guid InviteId { get; set; }
}
public class DeleteRequestToJoinTeamRequestDtoValidator : AbstractValidator<DeleteRequestToJoinTeamRequestDto>
{
    public DeleteRequestToJoinTeamRequestDtoValidator()
    {
        RuleFor(x => x.InviteId)
             .NotEmpty().WithMessage("InviteId is required");

    }
}
