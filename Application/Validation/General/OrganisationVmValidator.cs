using FluentValidation;
using Application.ViewModels.General;

namespace Application.Validation.General;
public class OrganisationVmValidator : AbstractValidator<OrganisationVm>
{
    public OrganisationVmValidator()
    {
        RuleFor(x => x.Make).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.SapNumber).NotEmpty();
    }
}
