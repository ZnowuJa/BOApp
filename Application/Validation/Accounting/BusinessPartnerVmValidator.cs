using Application.AdHocJobs;
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
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Pole 'Nazwa' jest wymagane.")
                .MinimumLength(3).WithMessage("Nazwa musi mieć co najmniej 3 znaki.")
                .MaximumLength(100).WithMessage("Nazwa może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.Street)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ulica jest wymagana.")
                .MinimumLength(3).WithMessage("Ulica musi mieć co najmniej 3 znaki.")
                .MaximumLength(100).WithMessage("Ulica może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Miasto jest wymagane.")
                .MinimumLength(3).WithMessage("Miasto musi mieć co najmniej 3 znaki.")
                .MaximumLength(50).WithMessage("Miasto może mieć maksymalnie 50 znaków.");

            RuleFor(x => x.PostalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kod pocztowy jest wymagany.")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Kod pocztowy musi być w formacie XX-XXX.");

            RuleFor(x => x.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Kraj jest wymagany.");

            RuleFor(x => x.BankAccountNumber)
                          .MustAsync(async (iban, cancellation) => await AppUtils.ValidateIbanAsync(iban))
                          .WithMessage("Nieprawidłowy numer IBAN.")
                          .When(x => !string.IsNullOrWhiteSpace(x.BankAccountNumber));

            RuleFor(x => x.VatId)
                .MinimumLength(6).When(x => !string.IsNullOrWhiteSpace(x.VatId)).WithMessage("VAT ID musi mieć co najmniej 6 znaków.")
                .MaximumLength(20).WithMessage("VAT ID może mieć maksymalnie 20 znaków.");

            RuleFor(x => x.SAPId)
                .MinimumLength(4).When(x => !string.IsNullOrWhiteSpace(x.SAPId)).WithMessage("SAP ID musi mieć co najmniej 4 znaki.")
                .MaximumLength(20).WithMessage("SAP ID może mieć maksymalnie 20 znaków.");

            RuleFor(x => x.BusinessPartnerType)
                .NotEmpty().WithMessage("Typ musi zostać wybrany.").NotEqual("Select...").WithMessage("Typ musi zostać wybrany.");

            RuleFor(x => x.BankTransferType)
                .NotEmpty().WithMessage("Typ musi zostać wybrany.").NotEqual("Select...").WithMessage("Typ musi zostać wybrany.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Lokalizacja musi zostać wybrana.");

            RuleFor(x => x.SAPFormType)
                .NotEmpty().WithMessage("SAPFormType musi zostać wybrany.").NotEqual("Select...").WithMessage("SAPFormType musi zostać wybrany.");

            RuleFor(x => x.DefaultCurrency)
                .NotEmpty().WithMessage("Waluta musi zostać wybrana.").NotEqual("Select...").WithMessage("Waluta musi zostać wybrana.");
        }
    }
}
