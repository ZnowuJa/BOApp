using Application.AdHocJobs;
using Application.Forms.Accounting;

using FluentValidation;

namespace Application.Validation.Accounting;
public class BankTransferFormVmValidator : AbstractValidator<BankTransferFormVm>
{
    public BankTransferFormVmValidator()
    {
        RuleFor(x => x.FormType).NotEmpty().WithMessage("Wybierz rodzaj dokumentu!");
        RuleFor(x => x.BankTransferMapping).NotNull().WithMessage("Dane przelewu są wymagane.");
        RuleFor(x => x.BankTransferMapping.BankAccountNumber)
            .MustAsync(async (accountNumber, cancellation) =>
                await AppUtils.ValidateIbanAsync(accountNumber))
            .WithMessage("Błędny numer konta!");
        //RuleFor(x => x.Description).NotEmpty().WithMessage("Wprowadź opis!");
        //RuleFor(x => x.OrganisationSapNumber).NotEmpty().WithMessage("Wybierz lub wprowadź numer lokacji!");

    }
}
