using Application.Forms.Accounting;

using FluentValidation;

namespace Application.Validation.Accounting;
public class BankTransferFormVmValidator : AbstractValidator<BankTransferFormVm>
{
    public BankTransferFormVmValidator()
    {
        RuleFor(x => x.FormType).NotEmpty().WithMessage("Wybierz rodzaj dokumentu!");
        //RuleFor(x => x.Description).NotEmpty().WithMessage("Wprowadź opis!");
        //RuleFor(x => x.OrganisationSapNumber).NotEmpty().WithMessage("Wybierz lub wprowadź numer lokacji!");

    }
}
