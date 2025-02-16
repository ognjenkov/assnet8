using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Teams.Request
{
    public class CreateTeamRequestDto
    {
        public required string Name { get; set; }
        //TODO image
    }
    public class CreateTeamRequestDtoValidator : AbstractValidator<CreateTeamRequestDto>
    {

        private readonly AppDbContext _dbContext;
        public CreateTeamRequestDtoValidator(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters")
                .MaximumLength(20).WithMessage("Name must be at most 20 characters")
                .MustAsync(IsUniqueName).WithMessage("Name is already taken");
        }
        private async Task<bool> IsUniqueName(string name, CancellationToken token)
        {
            return !await _dbContext.Teams.AnyAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken: token);
        }
    }
}

