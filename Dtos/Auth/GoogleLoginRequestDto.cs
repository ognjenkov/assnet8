using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Auth;

public class GoogleLoginRequestDto
{
    public required string Token { get; set; }
    public bool Persist { get; set; } = false;
}
public class GoogleLoginRequestDtoValidator : AbstractValidator<GoogleLoginRequestDto>
{
    public GoogleLoginRequestDtoValidator()
    {

    }
}