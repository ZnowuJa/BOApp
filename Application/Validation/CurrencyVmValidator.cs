using Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class CurrencyVmValidator : AbstractValidator<CurrencyVm>
{
    public CurrencyVmValidator()
    {

        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Define 3 letter currency!").MaximumLength(3).WithMessage("Define 3 letter currency!");
        RuleFor(x => x.Name).NotEqual("Select...");

    }
}