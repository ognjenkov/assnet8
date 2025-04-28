using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Listings.Request;

public class DeleteListingRequestDto
{
    public Guid ListingId { get; set; }

}
public class DeleteListingRequestDtoValidator : AbstractValidator<DeleteListingRequestDto>
{
    public DeleteListingRequestDtoValidator()
    {
        RuleFor(x => x.ListingId)
            .NotEmpty().WithMessage("ListingId is required");
    }
}