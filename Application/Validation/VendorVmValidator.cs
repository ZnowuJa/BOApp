using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class VendorVmValidator : AbstractValidator<VendorVm>
{
    public VendorVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(25);
        RuleFor(x => x.Name).NotEqual("Select...");
    }
}
