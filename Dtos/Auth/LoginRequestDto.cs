using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace assnet8.Dtos.Auth
{
    public class LoginRequestDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        private readonly AppDbContext _dbContext;
        public LoginRequestDtoValidator(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

            RuleFor(x => x.UsernameOrEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username or email is required")
                .MustAsync(UsernameOrEmailExists).WithMessage("Invalid username or email");
                
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(3).WithMessage("Invalid password")
                .MaximumLength(30).WithMessage("Invalid password");

        }

        private async Task<bool> UsernameOrEmailExists(string usernameOrEmail, CancellationToken token)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == usernameOrEmail.ToLower() || u.Email.ToLower() == usernameOrEmail.ToLower(), cancellationToken: token);
        }
    }
}