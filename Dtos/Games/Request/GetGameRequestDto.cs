using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Games.Request
{
    public class GetGameRequestDto
    {
        public Guid GameId { get; set; }
    }
    public class GetGameRequestDtoValidator : AbstractValidator<GetGameRequestDto>
    {
        public GetGameRequestDtoValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("FieldId is required");
        }
    }
}