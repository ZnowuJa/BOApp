using FluentValidation;
using Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class AssetVmByIdValidator : AbstractValidator<AssetVmById>
{
    public AssetVmByIdValidator()
    {
        //RuleFor(x => x.PartVm).NotEmpty();
        //RuleFor(x => x.InvoiceVm).NotEmpty();
        //RuleFor(x => x.StateVm).NotEmpty();
        //RuleFor(x => x.AssetTagNumber).NotEmpty();
        //RuleFor(x => x.SerialNumber).NotEmpty();
        //RuleFor(x => x.LastSeen).NotEmpty();
        //RuleFor(x => x.EmployeeForListVm).NotEmpty();
        //RuleFor(x => x.Price).NotEmpty();
        //RuleFor(x => x.CurrencyVm).NotEmpty();
        //RuleFor(x => x.PurchaseDate).NotEmpty();

        RuleFor(x => x.PartVmId).GreaterThan(0);
        RuleFor(x => x.InvoiceVmId).GreaterThan(0).WithMessage("select an Invoice please...");
        RuleFor(x => x.StateVmId).GreaterThan(0).WithMessage("select state please...");
        RuleFor(x => x.AssetTagNumber).NotEmpty();
        RuleFor(x => x.SerialNumber).NotEmpty();
        RuleFor(x => x.LastSeen).NotEmpty();
        RuleFor(x => x.EmployeeForListVmId).NotNull().NotEmpty().GreaterThan(0).WithMessage("ej no?");
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CurrencyVmId).NotEmpty().WithMessage("select an Invoice please...");
        RuleFor(x => x.PurchaseDate).NotEmpty();

    }

}
