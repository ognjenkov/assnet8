using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Services.Request;

public class DeleteServiceRequestDto
{
    public Guid ServiceId { get; set; }
}
public class DeleteServiceRequestDtoValidator : AbstractValidator<DeleteServiceRequestDto>
{
    public DeleteServiceRequestDtoValidator()
    {
        RuleFor(x => x.ServiceId)
            .NotEmpty().WithMessage("ServiceId is required");
    }
}