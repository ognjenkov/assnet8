using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Account.Request;

public class UpdatePasswordRequestDto
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}

public class UpdatePasswordRequestDtoValidator : AbstractValidator<UpdatePasswordRequestDto>
{
    public UpdatePasswordRequestDtoValidator()
    {
        RuleFor(x => x.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(3).WithMessage("Password must be at least 3 characters")
            .MaximumLength(30).WithMessage("Password must be at most 30 characters");

    }
}