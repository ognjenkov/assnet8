using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Listings.Request
{
    public class GetListingRequestDto
    {
        public Guid ListingId { get; set; }
    }
    public class GetListingRequestDtoValidator : AbstractValidator<GetListingRequestDto>
    {
        public GetListingRequestDtoValidator()
        {
            RuleFor(x => x.ListingId)
                .NotEmpty().WithMessage("ListingId is required");
        }
    }
}