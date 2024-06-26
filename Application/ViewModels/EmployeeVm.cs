﻿using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels;
public class EmployeeVm : IMapFrom<Employee>,IAssigneeVm
{
    public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string LongName { get; set; }
    public string Email { get; set; }  
    public Guid AzureObjectId { get; set; }
    public string? IdentityUserName { get; set; }
    public string? ThirdPartyId { get; set; }
    public int EnovaEmpId { get; set; }
    public string? Position { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int ManagerId { get; set; }
    public bool IsManager { get; set; }
    public ManagerVm Manager { get; set; }
    public EmployeeTypeVm? EmployeeTypeVm { get; set; }
    public int StatusId { get; set; }
    public int? IsActive { get; set; }
    public string? VcdCompanyNr { get; set; }
    public string? VcdempId { get; set; }
    public string? VcdempNumber { get; set; }
    public string? VcddeptNumber { get; set; }
    public string? SapNumber { get; set; }
    public string? FTEStartDate { get; set; }
    public string? FTEEndDate { get; set; }
    public string? ManagerEmail { get; set; }
    public string? DeptNumber { get; set; }
    public string? AspNetUserId { get; set; }
    public string typeName { get; set; } = "EmployeeVm";
    public List<string> Roles { get; set; }

    public EmployeeVm()
    {
        Id = 0;
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        AzureObjectId = Guid.Empty;
        IdentityUserName = string.Empty;
        EnovaEmpId = 0;
        MobileNumber = string.Empty;
        PhoneNumber = string.Empty;
        ManagerId = 0;
        IsManager = false;
        Manager = new ManagerVm();
        EmployeeTypeVm = new EmployeeTypeVm();
        IsActive = 0;
        VcdCompanyNr = string.Empty;
        VcdempId = string.Empty;
        VcdempNumber = string.Empty;
        VcddeptNumber = string.Empty;
        SapNumber = string.Empty;
        FTEStartDate = string.Empty;
        FTEEndDate = string.Empty;
        ManagerEmail = string.Empty;
        DeptNumber = string.Empty;
        AspNetUserId = string.Empty;
        Roles = new List<string>();
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeVm>()
            .ForMember(e => e.EmployeeTypeVm, z => z.MapFrom(src2 => src2.Type))
            .ForMember(e => e.LongName, z => z.MapFrom(src2 => src2.FirstName + " " + src2.LastName));
        profile.CreateMap<EmployeeVm, Employee>()
            .ForMember(e => e.Type, z => z.MapFrom(src2 => src2.EmployeeTypeVm));

    }
}
