using Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class CompanyTypeVmValidator : AbstractValidator<CompanyTypeVm>
{
    public CompanyTypeVmValidator()
    {

        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(10);
        RuleFor(x => x.Name).NotEqual("Select...");

    }
}