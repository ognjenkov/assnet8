using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Teams.Request;

public class GetTeamSimpleRequestDto
{
    public Guid TeamId { get; set; }

}
public class GetTeamSimpleRequestDtoValidator : AbstractValidator<GetTeamSimpleRequestDto>
{
    public GetTeamSimpleRequestDtoValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required");
    }
}