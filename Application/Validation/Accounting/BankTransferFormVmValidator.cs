using Application.AdHocJobs;
using Application.Forms.Accounting;
using Application.Forms.Accounting.Enums;

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
        RuleFor(x => x.Attachments)
            .Must((model, attachments) =>
            {
                var requiredCount = BankTransferTypes
                    .GetAttCountByDisplay(model.FormType);

                // if unknown type or no requirement, allow anything
                if (requiredCount == null)
                    return true;

                return attachments != null && attachments.Count >= requiredCount;
            })
            .WithMessage(model =>
            {
                var requiredCount = BankTransferTypes.GetAttCountByDisplay(model.FormType);
                var formType = model.FormType ?? "formularz";
                return $"Dla typu \"{formType}\" wymagane są co najmniej {requiredCount} załączniki.";
            });
        //RuleFor(x => x.Description).NotEmpty().WithMessage("Wprowadź opis!");
        //RuleFor(x => x.OrganisationSapNumber).NotEmpty().WithMessage("Wybierz lub wprowadź numer lokacji!");

    }
}
