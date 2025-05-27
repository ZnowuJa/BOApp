using Application.Forms;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Accounting
{
    public class AccountingNoteFormVmValidator : AbstractValidator<AccountingNoteFormVm>
    {
        public AccountingNoteFormVmValidator()
        {
            RuleFor(x => x.AmountPaid)
                .GreaterThanOrEqualTo(0).WithMessage("Kwota nie może być ujemna.");

            RuleFor(x => x.AmountRemaining)
                .GreaterThanOrEqualTo(0).WithMessage("Pozostała kwota nie może być ujemna.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Kwota jest wymagana.")
                .GreaterThan(0).WithMessage("Kwota musi być większa niż 0.");

            RuleFor(x => x.VIN)
                .NotEmpty().WithMessage("VIN jest wymagany.")
                .Matches("^[A-HJ-NPR-Z0-9]{17}$").WithMessage("VIN musi mieć dokładnie 17 znaków i nie może zawierać liter I, O, Q.");

            RuleFor(x => x.Registration)
                .NotEmpty().WithMessage("Numer rejestracyjny jest wymagany.");

            RuleFor(x => x.OrderNumber)
                .NotEmpty().WithMessage("Numer zlecenia jest wymagany.");

            RuleFor(x => x.DamageNumber)
                .NotEmpty().WithMessage("Numer szkody jest wymagany.");

            RuleFor(x => x.Date)
                .NotNull().WithMessage("Data jest wymagana.");

            RuleFor(x => x.PaymentDeadline)
                .Must((model, deadline) =>
                    !deadline.HasValue || !model.Date.HasValue || deadline >= model.Date)
                .WithMessage("Termin płatności nie może być późniejszy niż data główna.");


            RuleFor(x => x.NotesOrPayments)
                .MaximumLength(1000).WithMessage("Uwagi do wpłaty mogą mieć maksymalnie 1000 znaków.");

            RuleFor(x => x.NoteContent)
                .MaximumLength(1000).WithMessage("Treść noty może mieć maksymalnie 1000 znaków.");
        }
    }
}
