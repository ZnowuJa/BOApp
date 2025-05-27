using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class InvoiceItemValidator : AbstractValidator<InvoiceItemVm>
{
    public InvoiceItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.PartVmId).GreaterThan(0);
        RuleFor(x => x.UnitVmId).GreaterThan(0);

        //RuleFor(x => x.currencyVm.Title).NotEmpty();
        RuleFor(x => x.Qty).GreaterThan(0);
        //RuleFor(x => x.UnitVm.Title).NotEmpty();
    }
}
