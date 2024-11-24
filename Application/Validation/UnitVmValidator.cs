using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class UnitVmValidator : AbstractValidator<UnitVm>
{
    public UnitVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(10);
        RuleFor(x => x.Name).NotEqual("Select...");
        RuleFor(x => x.ShortName).NotNull().NotEmpty();
    }
}
