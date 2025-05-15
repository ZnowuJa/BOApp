using Application.ViewModels.Accounting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Accounting
{
    public class BusinessPartnerVmValidator : AbstractValidator<BusinessPartnerVm>
    {
        public BusinessPartnerVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Pole 'Nazwa' jest wymagane.")
                .MinimumLength(3).WithMessage("Nazwa musi mieć co najmniej 3 znaki.")
                .MaximumLength(100).WithMessage("Nazwa może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Ulica jest wymagana.")
                .MinimumLength(3).WithMessage("Ulica musi mieć co najmniej 3 znaki.")
                .MaximumLength(100).WithMessage("Ulica może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Miasto jest wymagane.")
                .MinimumLength(3).WithMessage("Miasto musi mieć co najmniej 3 znaki.")
                .MaximumLength(50).WithMessage("Miasto może mieć maksymalnie 50 znaków.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Kod pocztowy jest wymagany.")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Kod pocztowy musi być w formacie XX-XXX.");

            RuleFor(x => x.Country)
                .MinimumLength(2).When(x => !string.IsNullOrWhiteSpace(x.Country)).WithMessage("Kraj musi mieć co najmniej 2 znaki.")
                .MaximumLength(50).WithMessage("Kraj może mieć maksymalnie 50 znaków.");

            RuleFor(x => x.BankAccountNumber)
                .MinimumLength(10).When(x => !string.IsNullOrWhiteSpace(x.BankAccountNumber)).WithMessage("Numer konta musi mieć co najmniej 10 znaków.")
                .MaximumLength(34).WithMessage("Numer konta może mieć maksymalnie 34 znaki.");

            RuleFor(x => x.VatId)
                .MinimumLength(6).When(x => !string.IsNullOrWhiteSpace(x.VatId)).WithMessage("VAT ID musi mieć co najmniej 6 znaków.")
                .MaximumLength(20).WithMessage("VAT ID może mieć maksymalnie 20 znaków.");

            RuleFor(x => x.SAPId)
                .MinimumLength(4).When(x => !string.IsNullOrWhiteSpace(x.SAPId)).WithMessage("SAP ID musi mieć co najmniej 4 znaki.")
                .MaximumLength(20).WithMessage("SAP ID może mieć maksymalnie 20 znaków.");

/*            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Musisz wybrać poprawny typ partnera.");*/

            RuleFor(x => x.Location)
                .MaximumLength(10).WithMessage("Lokalizacja może mieć maksymalnie 10 znaków.");
        }
    }
}
