using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace assnet8.Dtos.Account.Request
{
    public class GetAccountCardRequestDto
    {
        public Guid UserId { get; set; }
    }
    public class GetAccountCardRequestDtoValidator : AbstractValidator<GetAccountCardRequestDto>
    {
        public GetAccountCardRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}