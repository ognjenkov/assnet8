using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Auth
{
    public class RegisterRequestDto
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        private readonly AppDbContext _dbContext;
        public RegisterRequestDtoValidator(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(20).WithMessage("Username must be at most 20 characters")
                .MustAsync(IsUniqueUsername).WithMessage("Username is already taken");
                
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters")
                .MaximumLength(20).WithMessage("Name must be at most 20 characters");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(IsUniqueEmail).WithMessage("Email is already taken");
            
            RuleFor(x => x.Password).NotEmpty();
        }

        private async Task<bool> IsUniqueUsername(string? username, CancellationToken token)
        {
            return !await _dbContext.Users.AnyAsync(u => u.Username == username);
        }
        private async Task<bool> IsUniqueEmail(string? email, CancellationToken token)
        {
            return !await _dbContext.Users.AnyAsync(u => u.Email == email);
        }
    }
}