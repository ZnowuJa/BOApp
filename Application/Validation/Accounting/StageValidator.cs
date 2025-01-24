using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting.BuisnessTravelSmallClasses;

using FluentValidation;

namespace Application.Validation.Accounting;
public class StageValidator : AbstractValidator<Stage>
{
    public StageValidator()
    {
        RuleFor(s => s.StartDate).NotEmpty().WithMessage("Wprowadź datę początkową etapu!");
        RuleFor(s => s.EndDate).NotEmpty().WithMessage("Wprowadź datę końcową etapu!");
        RuleFor(s => s).Must(s => s.EndDate > s.StartDate).WithMessage("Rozpoczęcie etapu nie może być wcześniejsze niż jego zakończenie.");

        //// Rule for both dates being null
        //RuleFor(s => s)
        //    .Must(s => s.StartDate.HasValue || s.EndDate.HasValue)
        //    .WithMessage("Wprowadź datę początkową i końcową etapu!")
        //    .When(s => !s.StartDate.HasValue && !s.EndDate.HasValue);

        //// Rule for StartDate being null
        //RuleFor(s => s.StartDate)
        //    .NotNull()
        //    .WithMessage("Wprowadź datę początkową etapu!")
        //    .When(s => s.EndDate.HasValue);

        //// Rule for EndDate being null
        //RuleFor(s => s.EndDate)
        //    .NotNull()
        //    .WithMessage("Wprowadź datę końcową etapu!")
        //    .When(s => s.StartDate.HasValue);

        //// Rule for EndDate > StartDate, only if both dates are not null
        //RuleFor(s => s)
        //    .Must(s => s.EndDate > s.StartDate)
        //    .WithMessage("Rozpoczęcie etapu nie może być wcześniejsze niż jego zakończenie.")
        //    .When(s => s.StartDate.HasValue && s.EndDate.HasValue);
    }
}



