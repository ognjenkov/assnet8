using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Users;

public class GetUserSimpleRequestDto
{
    public Guid UserId { get; set; }
}
public class GetUserSimpleRequestDtoValidator : AbstractValidator<GetUserSimpleRequestDto>
{
    public GetUserSimpleRequestDtoValidator()
    {
        RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId is required");
    }
}