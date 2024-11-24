using Application.ViewModels.General;
using FluentValidation;

namespace Application.Validation;
public class EmployeeVmValidator : AbstractValidator<EmployeeVm>
{
    public EmployeeVmValidator()
    {
        RuleFor(x => x.FirstName ).NotEmpty();
        RuleFor(x => x.LastName ).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.EmployeeTypeVm).NotEmpty();
        RuleFor(x => x.Manager).NotEmpty();

    }
}
