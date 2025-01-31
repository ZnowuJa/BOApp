using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.Accounting;
using FluentValidation;

namespace Application.Validation.Accounting;
public class VatRateVmValidator : AbstractValidator<VATRateVm>
{
    public VatRateVmValidator()
    {

        RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("Wybierz stawkę VAT!");
        


    }
}

