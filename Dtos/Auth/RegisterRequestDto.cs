using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace assnet8.Dtos.Auth;

public class RegisterRequestDto
{
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public IFormFile? ProfileImage { get; set; }
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
            .MaximumLength(30).WithMessage("Name must be at most 30 characters");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address")
            .MustAsync(IsUniqueEmail).WithMessage("Email is already taken");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(3).WithMessage("Password must be at least 3 characters")
            .MaximumLength(30).WithMessage("Password must be at most 30 characters");

        RuleFor(x => x.ProfileImage)
            .Cascade(CascadeMode.Stop)
            .Must(file => file == null || file.Length > 0).WithMessage("Image file cannot be empty")
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024).WithMessage("Image must be less than 5MB")
            .Must(file => file == null || new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.FileName).ToLower())).WithMessage("Only JPG and PNG images are allowed");
    }

    private async Task<bool> IsUniqueUsername(string username, CancellationToken token)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower(), cancellationToken: token);
    } /// born to shit forced to wipe, ovde treba da se koristi string.Equals(,,StringComparison.InvariantCultureIgnoreCase) ali database ne podrzava
    private async Task<bool> IsUniqueEmail(string email, CancellationToken token)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken: token);
    }
}