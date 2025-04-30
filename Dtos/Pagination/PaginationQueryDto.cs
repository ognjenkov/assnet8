using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Pagination;

public class PaginationQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 18;
}
public class PaginationQueryDtoValidator : AbstractValidator<PaginationQueryDto>
{
    public PaginationQueryDtoValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(18).WithMessage("Page size must be greater than or equal to 18.")
            .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
    }
}