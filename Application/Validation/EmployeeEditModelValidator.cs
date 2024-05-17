﻿using Application.ViewModelsForForms;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;
public class EmployeeEditModelValidator : AbstractValidator<EmployeeEditModel>
{
    public EmployeeEditModelValidator()
    {
        RuleFor(x => x.employeeVm.FirstName).NotEmpty();
        RuleFor(x => x.employeeVm.LastName).NotEmpty();
        RuleFor(x => x.employeeVm.Email).EmailAddress();
        RuleFor(x => x.employeeVm.MobileNumber).NotEmpty();
        RuleFor(x => x.employeeVm.PhoneNumber).NotEmpty();

        //RuleFor(x => x.managerVm).SetValidator(new ManagerVmValidator());
        
    }

}
