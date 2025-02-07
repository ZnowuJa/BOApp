using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateEmployeeCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if(request.EmployeeVm != null)
        {
            var empCoC = await _appDbContext.Employees.Where(p => p.Id == request.EmployeeVm.Id).FirstOrDefaultAsync();
            empCoC.CoCGroupId = request.EmployeeVm.CoCGroupId;
            //empCoC.BankAccountNumber = request.EmployeeVm.BankAccountNumber;
            _appDbContext.Employees.Update(empCoC);

            await _appDbContext.SaveChangesAsync();
            return empCoC.Id;
        }
        var type = await _appDbContext.EmployeeTypes.Where(p => p.Id == request.Type.Id).FirstOrDefaultAsync();
        var item = await _appDbContext.Employees.Where(p => p.Id == request.Id).FirstOrDefaultAsync();


        item.FirstName = request.FirstName;
        item.LastName = request.LastName;
        item.Email = request.Email;
        item.MobileNumber = request.MobileNumber;
        item.PhoneNumber = request.PhoneNumber;
        item.ManagerId = request.ManagerId;
        //try
        //{
        //    item.BankAccountNumber = request.EmployeeVm.BankAccountNumber;
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e.Message);
        //}

        item.Type = type;

        _appDbContext.Employees.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
