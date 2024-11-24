using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class CompanyVmValidator : AbstractValidator<CompanyVm>
{
    public CompanyVmValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(10);
        RuleFor(x => x.Name).NotEqual("Select...");
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.VATID).NotEmpty().MinimumLength(12).MaximumLength(12);
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.Building).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.CountryCode).NotEmpty();
        RuleFor(x => x.ContactPerson).NotEmpty();
        RuleFor(x => x.ContactPersonMobile).NotEmpty();
        RuleFor(x => x.ContactPersonEmail).NotEmpty().EmailAddress();
    }
}