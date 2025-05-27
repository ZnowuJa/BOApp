using Application.ViewModels.General;
using FluentValidation;

namespace Application.Validation;
public class ManagerVmValidator : AbstractValidator<ManagerVm>
{
    public ManagerVmValidator()
    {
        RuleFor(x => x.Email).NotEmpty();

    }

}
