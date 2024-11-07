using Application.ViewModels.Accounting;
using Domain.Entities.Accounting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class CountryVmValidator : AbstractValidator<CountryVm>
    {
        public CountryVmValidator()
        {
            RuleFor(country => country.Code).NotEmpty().MinimumLength(3);
            RuleFor(country => country.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(country => country.IsEU).NotNull();
        }
    }
}
