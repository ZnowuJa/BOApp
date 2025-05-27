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
        
        When(x => x.IsManager, () =>
        {
            RuleFor(x => x.PersonalDeptNumber).Must(val => val.Length == 3 || val.Length == 6)
            .WithMessage("The value must be either 3 or 6 characters long.");
            RuleFor(x => x.DeptNumber).Must(val => val.Length == 3 || val.Length == 6)
            .WithMessage("The value must be either 3 or 6 characters long.");
        });

    }
}
