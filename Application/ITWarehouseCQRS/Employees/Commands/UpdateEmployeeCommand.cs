﻿using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Commands;
public class UpdateEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int? ManagerId { get; set; }
    public EmployeeTypeVm? Type { get; set; }
    public string? DG { get; set; } = string.Empty;
    public string? CC { get; set; } = string.Empty;

    public UpdateEmployeeCommand(
        int id, 
        string firstName, 
        string lastName, 
        string email,   
        string mobileNumber, 
        string phoneNumber, 
        int managerId, 
        EmployeeTypeVm type
        )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        MobileNumber = mobileNumber;
        PhoneNumber = phoneNumber;
        ManagerId = managerId;
        Type = type;
    }
}