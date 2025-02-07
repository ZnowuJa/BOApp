using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class UpdateEmployeeBankAccountCommand : IRequest<int>
{
    public UpdateEmployeeBankAccountCommand(int empId, string bankAccount)
    {
        EmpId = empId;
        BankAccount = bankAccount;
    }

    public int EmpId { get; }
    public string BankAccount { get; }
}

