using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class CurrencyVmValidator : AbstractValidator<CurrencyVm>
{
    public CurrencyVmValidator()
    {

        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Define 3 letter currency!").MaximumLength(3).WithMessage("Define 3 letter currency!");
        RuleFor(x => x.Name).NotEqual("Select...");

    }
}