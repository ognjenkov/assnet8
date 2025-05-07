using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Invites.Request;

public class RequestToJoinTeamRequestDto
{
    public Guid TeamId { get; set; }
}
public class RequestToJoinTeamRequestDtoValidator : AbstractValidator<RequestToJoinTeamRequestDto>
{
    public RequestToJoinTeamRequestDtoValidator()
    {
        RuleFor(x => x.TeamId)
             .NotEmpty().WithMessage("TeamId is required");
    }
}