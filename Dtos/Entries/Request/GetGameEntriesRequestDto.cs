using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Entries.Request;

public class GetGameEntriesRequestDto
{
    public Guid GameId { get; set; }
}
public class GetGameEntriesRequestDtoValidator : AbstractValidator<GetGameEntriesRequestDto>
{
    public GetGameEntriesRequestDtoValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required");
    }
}