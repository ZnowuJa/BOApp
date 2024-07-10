using Application.ViewModels.General;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class ManagerVmValidator : AbstractValidator<ManagerVm>
{
    public ManagerVmValidator()
    {
        RuleFor(x => x.Email).NotEmpty();

    }

}
