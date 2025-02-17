using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Teams.Request
{
    public class GetTeamRequestDto
    {
        public Guid TeamId { get; set; }
    }
    public class GetTeamRequestDtoValidator : AbstractValidator<GetTeamRequestDto>
    {
        public GetTeamRequestDtoValidator()
        {
            RuleFor(x => x.TeamId)
                .NotEmpty().WithMessage("TeamId is required");
        }
    }
}