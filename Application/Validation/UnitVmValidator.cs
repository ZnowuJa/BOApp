using Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class UnitVmValidator : AbstractValidator<UnitVm>
{
    public UnitVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(10);
        RuleFor(x => x.Name).NotEqual("Select...");
        RuleFor(x => x.ShortName).NotNull().NotEmpty();
    }
}
