using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Entries.Request
{
    public class CreateEntryRequestDto
    {
        public int OpNumber { get; set; }
        public int RentNumber { get; set; }
        public string? Message { get; set; }
        public Guid GameId { get; set; }
    }
    public class CreateEntryRequestDtoValidator : AbstractValidator<CreateEntryRequestDto>
    {

        public CreateEntryRequestDtoValidator()
        {
            RuleFor(x => x.Message)
                .MaximumLength(200).WithMessage("Message cannot be longer than 200 characters");

            RuleFor(x => x.OpNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Op number cannot be negative")
                .LessThanOrEqualTo(50).WithMessage("Op number cannot be greater than 50");

            RuleFor(x => x.RentNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Rent number cannot be negative")
                .LessThanOrEqualTo(50).WithMessage("Rent number cannot be greater than 50");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required");
        }

    }
}