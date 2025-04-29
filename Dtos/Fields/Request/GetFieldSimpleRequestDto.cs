using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Fields.Request;

public class GetFieldSimpleRequestDto
{
    public Guid FieldId { get; set; }
}
public class GetFieldSimpleRequestDtoValidator : AbstractValidator<GetFieldSimpleRequestDto>
{
    public GetFieldSimpleRequestDtoValidator()
    {
        RuleFor(x => x.FieldId)
            .NotEmpty().WithMessage("FieldId is required");
    }
}