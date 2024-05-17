﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ITWarehouse;
public partial class EmpInfo
{
    public int Id { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? EmpId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? DeptNumber { get; set; }

    public DateOnly? EmpFrom { get; set; }

    public DateOnly? EmpTo { get; set; }

    public string? Position { get; set; }

    public string? Manager { get; set; }
    public int? ManagerEmpId { get; set; }
    public string? ManagerUserName { get; set; }

    public int? Holiday { get; set; }

    public int? IsManager { get; set; }

    public int? IsActive { get; set; }

    public string? CostCenter { get; set; }

    public string? CompanySAP { get; set; }

    public string? VcdempId { get; set; }

    public string? VcdempNumber { get; set; }

    public string? VcddeptNumber { get; set; }

    public string? SapnumberHr { get; set; }

}