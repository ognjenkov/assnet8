using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Fields.Request
{
    public class GetFieldRequestDto
    {
        public Guid FieldId { get; set; }
    }
    public class GetFieldRequestDtoValidator : AbstractValidator<GetFieldRequestDto>
    {
        public GetFieldRequestDtoValidator()
        {
            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("FieldId is required");
        }
    }
}