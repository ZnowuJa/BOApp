using Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class NoteVmValidator : AbstractValidator<NoteVm>
{
    public NoteVmValidator()
    {
        RuleFor(x => x.Text).MinimumLength(3).MaximumLength(500);
        
    }
}
