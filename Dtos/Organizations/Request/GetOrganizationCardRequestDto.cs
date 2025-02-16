using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Organizations.Request
{
    public class GetOrganizationCardRequestDto
    {
        public Guid OrganizationId { get; set; }
    }
    public class GetOrganizationCardRequestDtoValidator : AbstractValidator<GetOrganizationCardRequestDto>
    {
        public GetOrganizationCardRequestDtoValidator()
        {
            RuleFor(x => x.OrganizationId)
                 .NotEmpty().WithMessage("OrganizationId is required");
        }
    }
}