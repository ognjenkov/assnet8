using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Games.Request;

public class DeleteGameRequestDto
{
    public Guid GameId { get; set; }
}
public class DeleteGameRequestDtoValidator : AbstractValidator<DeleteGameRequestDto>
{
    public DeleteGameRequestDtoValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required");
    }
}