using FluentValidation;

namespace Application.Accounts.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Matches(@"^[^@]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$")
            .WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(20)
            .Matches(@"[\W_]")
            .WithMessage("Password must contain at least one non-alphanumeric character.");
    }
}
