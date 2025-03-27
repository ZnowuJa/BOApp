using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.Accounting;
using FluentValidation;

namespace Application.Validation.Accounting;
public class BillValidator : AbstractValidator<Bill>
{
    public BillValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Wprowadź kwotę!");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Wprowadź uzasadnienie!");
        //RuleFor(x => x.FilePath)
        //    .NotEmpty().WithMessage("Załącz plik z dokumentem!");
        RuleFor(x => x.BillFiles)
        .NotEmpty().WithMessage("Załącz co najmniej jeden plik z dokumentem!");
        RuleForEach(x => x.BillFiles).SetValidator(new BillFileValidator());
        When(b => b.Invoice, () =>
        {
            RuleFor(b => b.InvoiceDate).NotEmpty().WithMessage("Uzupełnij datę wystawienia faktury!");

            RuleFor(b => b.InvoiceNumber).NotEmpty().WithMessage("Wprowadź numer faktury!");
            RuleFor(b => b.BusinessPartner.Name).NotEmpty().WithMessage("Wprowadź nazwę wystawcy faktury!");
            RuleFor(b => b.BusinessPartner.VatId)
                .NotEmpty().WithMessage("Numer NIP wystawcy z kodem kraju!").DependentRules(() =>
                {
                    RuleFor(b => b.BusinessPartner.VatId).Matches(@"\b(ATU\d{8}|BE[01]\d{9}|BG\d{9,10}|CY\d{8}[LX]|CZ\d{8,10}|DE\d{9}|DK\d{8}|EE\d{9}|EL\d{9}|ES[\dA-Z]\d{7}[\dA-Z]|FI\d{8}|FR[\dA-Z]{2}\d{9}|HR\d{11}|HU\d{8}|IE\d{7}[A-Z]{2}|IT\d{11}|LT(\d{9}|\d{12})|LU\d{8}|LV\d{11}|MT\d{8}|NL\d{9}B\d{2}|PL\d{10}|PT\d{9}|RO\d{2,10}|SE\d{12}|SI\d{8}|SK\d{10})\b").WithMessage("Niepoprawny numer NIP!");
                });

        });
        When(b => b.isParking, () =>
        {
            RuleFor(b => b.ParkingAmount).GreaterThan(0).WithMessage("Wprowadź kwotę za parking!");
        }
            );

        //RuleForEach(x => x.InvoiceMappings).SetValidator(new InvoiceMappingValidator());
        
    }
    //public BillValidator(DateTime? start, DateTime? end)
    //{
    //    RuleFor(x => x.InvoiceDate).GreaterThanOrEqualTo(start.Value)
    //                .LessThanOrEqualTo(end.Value)
    //                .WithMessage("Data faktury powinna być między {PropertyValue} a {ComparisonValue}.");

    //}
}

public class BillFileValidator : AbstractValidator<BillFile>
{
    public BillFileValidator()
    {
        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("Ścieżka pliku nie może być pusta!");

        RuleFor(x => x.OriginalFileName)
            .NotEmpty().WithMessage("Nazwa pliku nie może być pusta!");

        RuleFor(x => x.AttUrl)
            .NotEmpty().WithMessage("URL załącznika nie może być pusty!");
    }
}

