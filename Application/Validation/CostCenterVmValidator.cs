using Application.ViewModels;
using Application.ViewModels.Accounting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class CostCenterVmValidator : AbstractValidator<CostCenterVm>
    {
        public CostCenterVmValidator()
        {
            RuleFor(x => x.MPK).NotEmpty();
            RuleFor(x => x.Description).MinimumLength(3).WithMessage("Description needs to be at least 3 chars!");
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
