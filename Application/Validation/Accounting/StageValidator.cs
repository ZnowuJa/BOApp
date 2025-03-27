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
        //RuleFor(s => s.EndDate).NotEmpty().WithMessage("Wprowadź datę końcową etapu!");
        //RuleFor(s => s).Must(s => s.EndDate > s.StartDate).WithMessage("Rozpoczęcie etapu nie może być wcześniejsze niż jego zakończenie.");
        RuleFor(s => s.EndDate)
            .NotEmpty().WithMessage("Wprowadź datę końcową etapu!");
        //RuleFor(s => s.TimeSpanOK)
            //.Must(timeSpanOK => !timeSpanOK)
            //.WithMessage("Rozpoczęcie etapu nie może być późniejsze niż jego zakończenie.");
       
    }
    public StageValidator(List<Stage> stages)
    {
        RuleFor(stage => stage)
            .Must((stage, context) => !HasOverlappingPeriods(stage, stages))
            .WithMessage("The stage periods must not overlap.");
    }
    private bool HasOverlappingPeriods(Stage currentStage, List<Stage> stages)
    {
        foreach (var stage in stages)
        {
            if (stage.Id != currentStage.Id &&
                stage.StartDate.HasValue && stage.EndDate.HasValue &&
                currentStage.StartDate.HasValue && currentStage.EndDate.HasValue)
            {
                if (currentStage.StartDate < stage.EndDate && currentStage.EndDate > stage.StartDate)
                {
                    return true;
                }
            }
        }
        return false;
    }
}



