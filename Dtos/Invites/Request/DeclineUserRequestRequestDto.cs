using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class DeclineUserRequestRequestDto
{
    public Guid InviteId { get; set; }
}
public class DeclineUserRequestRequestDtoValidator : AbstractValidator<DeclineUserRequestRequestDto>
{
    public DeclineUserRequestRequestDtoValidator()
    {
        RuleFor(x => x.InviteId)
             .NotEmpty().WithMessage("InviteId is required");

    }
}