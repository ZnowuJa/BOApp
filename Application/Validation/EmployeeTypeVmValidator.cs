using Application.ViewModels.General;
using FluentValidation;

namespace Application.Validation;
public class EmployeeTypeVmValidator : AbstractValidator<EmployeeTypeVm>
{
    public EmployeeTypeVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(10);
        RuleFor(x => x.Name).NotEqual("Select...");
    }
}
