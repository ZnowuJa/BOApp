using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class CategoryVmValidator : AbstractValidator<CategoryVm>
{
    public CategoryVmValidator()
    {

        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(15);
        RuleFor(x => x.Name).NotEqual("Select...");
        RuleFor(x => x.Prefix).NotEmpty().MinimumLength(3).MaximumLength(8);
        RuleFor(x => x.LeadingZeros).NotEmpty();
        RuleFor(x => x.CategoryTypeVm).SetValidator(new CategoryTypeVmValidator());
    }
}