using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Users;

public class GetUserCardRequestDto
{
    public Guid UserId { get; set; }
}
public class GetUserCardRequestDtoValidator : AbstractValidator<GetUserCardRequestDto>
{
    public GetUserCardRequestDtoValidator()
    {
        RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId is required");
    }
}