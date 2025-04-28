using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Fields.Request;

public class DeleteFieldRequestDto
{
    public Guid FieldId { get; set; }
}
public class DeleteFieldRequestDtoValidator : AbstractValidator<DeleteFieldRequestDto>
{
    public DeleteFieldRequestDtoValidator()
    {
        RuleFor(x => x.FieldId)
            .NotEmpty().WithMessage("FieldId is required");
    }
}