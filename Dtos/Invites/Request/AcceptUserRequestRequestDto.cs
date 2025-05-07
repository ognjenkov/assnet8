using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class AcceptUserRequestRequestDto
{
    public Guid InviteId { get; set; }
}
public class AcceptUserRequestRequestDtoValidator : AbstractValidator<AcceptUserRequestRequestDto>
{
    public AcceptUserRequestRequestDtoValidator()
    {
        RuleFor(x => x.InviteId)
             .NotEmpty().WithMessage("InviteId is required");

    }
}