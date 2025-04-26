using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Images.Request;

public class GetImageRequestDto
{
    public required Guid ImageId { get; set; }
}
public class GetImageRequestDtoValidator : AbstractValidator<GetImageRequestDto>
{
    public GetImageRequestDtoValidator()
    {
        RuleFor(x => x.ImageId)
            .NotEmpty().WithMessage("ImageId is required");
    }
}