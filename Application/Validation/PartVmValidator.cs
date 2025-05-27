using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class PartVmValidator : AbstractValidator<PartVm>
{
    public PartVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(4).MaximumLength(25);
        RuleFor(x => x.Name).NotEqual("Select...");
        //RuleFor(x => x.CategoryVm).SetValidator(new CategoryVmValidator());
        //RuleFor(x => x.VendorVm).SetValidator(new VendorVmValidator());
        RuleFor(x => x.CategoryVm.Name).NotEqual("Select...");
        RuleFor(x => x.VendorVm.Name).NotEqual("Select...");
        RuleFor(x => x.Description).MinimumLength(5).MaximumLength(250);
        // RuleFor(x => x.Photo).MinimumLength(5).MaximumLength(250);
        RuleFor(x => x.WarrantyPeriod).NotEqual(9999).GreaterThan(0);
    }
}
