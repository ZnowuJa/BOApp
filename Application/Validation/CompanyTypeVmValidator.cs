using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class CompanyTypeVmValidator : AbstractValidator<CompanyTypeVm>
{
    public CompanyTypeVmValidator()
    {

        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(10);
        RuleFor(x => x.Name).NotEqual("Select...");

    }
}