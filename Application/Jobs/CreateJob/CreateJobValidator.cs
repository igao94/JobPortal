using FluentValidation;

namespace Application.Jobs.CreateJob;

public class CreateJobValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobValidator()
    {
        RuleFor(j => j.Title).NotEmpty();
        RuleFor(j => j.Description).NotEmpty();
        RuleFor(j => j.CompanyName).NotEmpty();
        RuleFor(j => j.Location).NotEmpty();
    }
}
