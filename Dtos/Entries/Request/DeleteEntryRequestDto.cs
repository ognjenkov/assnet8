using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Entries.Request
{
    public class DeleteEntryRequestDto
    {
        public Guid EntryId { get; set; }
    }
    public class DeleteEntryRequestDtoValidator : AbstractValidator<DeleteEntryRequestDto>
    {
        public DeleteEntryRequestDtoValidator()
        {
            RuleFor(x => x.EntryId)
                .NotEmpty().WithMessage("EntryId is required");
        }
    }
}