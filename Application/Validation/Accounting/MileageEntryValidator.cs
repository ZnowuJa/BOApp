
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;
using Application.ViewModels.Accounting;

using FluentValidation;

using Microsoft.Graph.Models;

namespace Application.Validation.Accounting;
public class MileageEntryValidator : AbstractValidator<MileageRegisterEntry>
{
    public MileageEntryValidator()
    {

        RuleFor(s => s.DateTimeForBinding).NotEmpty().WithMessage("Uzupenij datę przejazdu!");
        RuleFor(s => s.Mileage).NotEmpty().WithMessage("Podaj liczbę przejechanych kilometrów").GreaterThanOrEqualTo(1).WithMessage("Uzupełnij liczbę przejechanych kilometrów");
        RuleFor(s => s.Purpose).NotEmpty().WithMessage("Uzupełnij cel przejazdu!");
        RuleFor(s => s.RouteDescription).NotEmpty().WithMessage("Uzupełnij opis przejazdu!");

    }
}

public class MileageRegisterValidator : AbstractValidator<MileageRegister>
{

    public MileageRegisterValidator()
    {
        RuleForEach(x => x.Entries).SetValidator(new MileageEntryValidator());
        RuleFor(x => x.Entries)
            .Must(entries => entries != null && entries.Any())
            .WithMessage("Proszę dodaj co najmniej jeden przejazd.");
    }
}

