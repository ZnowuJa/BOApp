using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class CreateCategoryTypeCommandValidator : AbstractValidator<CreateCategoryTypeCommand>
{
    public CreateCategoryTypeCommandValidator()
    {
        RuleFor(x=> x.Name).NotEmpty().MaximumLength(10);
    }
}
