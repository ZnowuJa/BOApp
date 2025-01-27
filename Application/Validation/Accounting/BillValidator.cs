using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.Accounting;
using FluentValidation;

namespace Application.Validation.Accounting;
public class BillValidator : AbstractValidator<Bill>
{
    public BillValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Wprowadź kwotę!");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Wprowadź uzasadnienie!");
        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("Załącz plik z dokumentem!");

        RuleForEach(x => x.InvoiceMappings).SetValidator(new InvoiceMappingValidator());
        
    }
}

