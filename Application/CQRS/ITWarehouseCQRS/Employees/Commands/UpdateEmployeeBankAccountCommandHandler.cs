using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class UpdateEmployeeBankAccountCommandHandler : IRequestHandler<UpdateEmployeeBankAccountCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateEmployeeBankAccountCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateEmployeeBankAccountCommand request, CancellationToken cancellationToken)
    {
        int empId = request.EmpId;
        var emp = await _appDbContext.Employees.Where(e => e.EnovaEmpId == request.EmpId).FirstOrDefaultAsync();
        emp.BankAccountNumber = request.BankAccount;

        _appDbContext.Employees.Update(emp);
        _appDbContext.SaveChangesAsync();

        return empId;
    }
}

