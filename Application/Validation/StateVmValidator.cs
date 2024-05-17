using Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class StateVmValidator : AbstractValidator<StateVm>
{
    public StateVmValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(12);
        RuleFor(x => x.Name).NotEqual("Select...");
        //RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}
