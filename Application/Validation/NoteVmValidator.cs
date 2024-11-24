using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class NoteVmValidator : AbstractValidator<NoteVm>
{
    public NoteVmValidator()
    {
        RuleFor(x => x.Text).MinimumLength(3).MaximumLength(500);
        
    }
}
