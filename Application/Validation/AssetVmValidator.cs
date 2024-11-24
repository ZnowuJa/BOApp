using FluentValidation;
using Application.ViewModels;

namespace Application.Validation;
public class AssetVmValidator : AbstractValidator<AssetVm>
{
    public AssetVmValidator()
    {

        RuleFor(x => x.PartVmId).GreaterThan(0);
        RuleFor(x => x.InvoiceVmId).GreaterThan(0);
        RuleFor(x => x.StateVmId).GreaterThan(0);
        RuleFor(x => x.AssetTagNumber).NotEmpty();
        RuleFor(x => x.SerialNumber).NotEmpty();
        RuleFor(x => x.LastSeen).NotEmpty();
        //RuleFor(x => x.EmployeeVm.Email).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CurrencyVmId).GreaterThan(0);
        RuleFor(x => x.PurchaseDate).NotEmpty();

    }

}
