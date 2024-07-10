using Application.ViewModels.General;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class EmployeeVmValidator : AbstractValidator<EmployeeVm>
{
    public EmployeeVmValidator()
    {
        RuleFor(x => x.FirstName ).NotEmpty();
        RuleFor(x => x.LastName ).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.EmployeeTypeVm).NotEmpty();
        RuleFor(x => x.Manager).NotEmpty();

    }
}
