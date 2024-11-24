using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class InvoiceVmValidator : AbstractValidator<InvoiceVm>
{
    public InvoiceVmValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MinimumLength(3);
        RuleFor(x => x.CompanyVm.Id).NotEmpty();
        RuleFor(x => x.CompanyVm.Name).NotEmpty().WithMessage("Please select company...");
        RuleFor(x => x.CompanyVm.Name).NotEqual("Select...").WithMessage("Please select company...");
        RuleFor(x => x.Date).NotEmpty();
        //RuleFor(x => x.TotalNet).GreaterThan(0);
        
    }
}
