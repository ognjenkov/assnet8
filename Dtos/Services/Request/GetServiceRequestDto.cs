using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Services.Request
{
    public class GetServiceRequestDto
    {
        public Guid ServiceId { get; set; }
    }
    public class GetServiceRequestDtoValidator : AbstractValidator<GetServiceRequestDto>
    {
        public GetServiceRequestDtoValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("ServiceId is required");
        }
    }
}