using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class StateVmValidator : AbstractValidator<StateVm>
{
    public StateVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(12);
        RuleFor(x => x.Name).NotEqual("Select...");
        //RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}
