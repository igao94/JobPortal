using FluentValidation;

namespace Application.Jobs.UpdateJob;

public class UpdateJobValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobValidator()
    {
        RuleFor(j => j.Title).NotEmpty();
        RuleFor(j => j.Description).NotEmpty();
        RuleFor(j => j.CompanyName).NotEmpty();
        RuleFor(j => j.Location).NotEmpty();
    }
}
