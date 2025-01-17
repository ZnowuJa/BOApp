using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;
using FluentValidation;

namespace Application.Validation.Accounting;
public class BusinessTravelFormVmValidator : AbstractValidator<BusinessTravelFormVm>
{
    public BusinessTravelFormVmValidator()
    {
        RuleFor(x => x)
            .Must(x => x.StartDate < x.EndDate)
            .WithMessage("Start Date must be earlier than End Date.");
       
    }
}
