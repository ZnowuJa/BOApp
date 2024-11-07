using Application.ViewModels;
using FluentValidation;

namespace Application.Validation;
public class CategoryTypeVmValidator : AbstractValidator<CategoryTypeVm>
{
    public CategoryTypeVmValidator()
    {

        //RuleFor(x => x.Title).MinimumLength(3).MaximumLength(11);
        //RuleFor(x => x.Title).NotEqual("Select Category EmployeeTypeVm...").WithMessage("Select Category EmployeeTypeVm!");
        //RuleFor(x => x.Title).NotEmpty().MinimumLength(3).WithMessage("AV:: Title should have 3-10 characters!").MaximumLength(10).WithMessage("AV :: Title should have 3-10 characters!");
        //;

        RuleFor(x => x.Name).Custom((value, context) =>
        {
            // Your custom validation logic for the 'Title' field
            if (value.Length > 10 || value.Length < 3)
            {
                context.AddFailure("Title", "Title should be 3 - 10 characters...");
            }
        });
        RuleFor(x => x.Name).NotEqual("Select...");

        //RuleFor(x => x.Description).Custom((value, _appDbContext) =>
        //{
        //    // Your custom validation logic for the 'Description' field
        //    if (value.Length > 20)
        //    {
        //        _appDbContext.AddFailure("Description", "Description should be less than 20 characters");
        //    }
        //});

    }
}