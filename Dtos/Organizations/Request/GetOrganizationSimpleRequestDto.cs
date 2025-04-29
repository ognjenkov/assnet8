using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Organizations.Request;

public class GetOrganizationSimpleRequestDto
{
    public Guid OrganizationId { get; set; }
}
public class GetOrganizationSimpleRequestDtoValidator : AbstractValidator<GetOrganizationSimpleRequestDto>
{
    public GetOrganizationSimpleRequestDtoValidator()
    {
        RuleFor(x => x.OrganizationId)
             .NotEmpty().WithMessage("OrganizationId is required");
    }
}