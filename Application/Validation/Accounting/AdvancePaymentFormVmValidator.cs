using Application.Forms.Accounting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Accounting
{
    public class AdvancePaymentFormVmValidator : AbstractValidator<AdvancePaymentFormVm>
    {
        public AdvancePaymentFormVmValidator()
        {
            RuleFor(x => x.Objective)
                   .NotEmpty().WithMessage("Cel nie może być pusty!");

            RuleFor(x => x.AdvancePaymentAmount)
                   .NotNull().WithMessage("Kwota zaliczki jest wymagana!")
                   .GreaterThan(0).WithMessage("Kwota zaliczki musi być większa niż 0!");

            When(form => form.AdvancePaymentCash == false, () =>
            {
                RuleFor(x => x.CashPoint).ChildRules(cashpoint =>
                {
                    cashpoint.RuleFor(c => c.SapNumber).NotEmpty().WithMessage("Wybierz kasę!");
                });

            });
/*
            When(form => form.AdvancePaymentCash == true, () =>
            {
                RuleFor(x => x.BankAccountNumber)
                .Custom((bankAccountNumber, context) =>
                {
                    if (string.IsNullOrEmpty(bankAccountNumber) || !System.Text.RegularExpressions.Regex.IsMatch(bankAccountNumber, "^PL\\d{26}$"))
                    {
                        context.AddFailure("Wprowadź poprawny numer konta!");
                    }
                });
            });*/

                When(form => form.Status == "ZaliczkaKasa", () =>
            {
                RuleFor(x => x.CashPayoutNumber).NotEmpty().WithMessage("Wprowadź numer dokumentu kasowego z Austostacji!");
            });
        }
    }
}
