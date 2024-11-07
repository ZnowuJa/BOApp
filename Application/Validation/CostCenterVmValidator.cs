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
            RuleFor(x => x.MPK).MinimumLength(3);
            RuleFor(x => x.Description).MinimumLength(3);
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
