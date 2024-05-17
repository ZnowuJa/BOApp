using Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class InvoiceItemValidator : AbstractValidator<InvoiceItemVm>
{
    public InvoiceItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.PartVmId).GreaterThan(0);
        RuleFor(x => x.UnitVmId).GreaterThan(0);

        //RuleFor(x => x.CurrencyVm.Name).NotEmpty();
        RuleFor(x => x.Qty).GreaterThan(0);
        //RuleFor(x => x.UnitVm.Name).NotEmpty();
    }
}
