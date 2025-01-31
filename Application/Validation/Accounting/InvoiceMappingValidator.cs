using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.Accounting;
using FluentValidation;

namespace Application.Validation.Accounting;
public class InvoiceMappingValidator : AbstractValidator<InvoiceMapping>
{
    public InvoiceMappingValidator()
    {
        RuleFor(x => x.AmountNet)
            .GreaterThan(0).WithMessage("Wprowadź kwotę!");

        RuleFor(x => x.VATRate).SetValidator(new VatRateVmValidator());
        RuleFor(x => x.VATRate).NotNull().WithMessage("Wybierz stawkę VAT!");

    }

}

