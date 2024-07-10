using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Employees.Commands;
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
        var type = await _appDbContext.EmployeeTypes.Where(p => p.Id == request.Type.Id).FirstOrDefaultAsync();
        var item = await _appDbContext.Employees.Where(p => p.Id == request.Id).FirstOrDefaultAsync();


        item.FirstName = request.FirstName;
        item.LastName = request.LastName;
        item.Email = request.Email;
        item.MobileNumber = request.MobileNumber;
        item.PhoneNumber = request.PhoneNumber;
        item.ManagerId = request.ManagerId;
        item.Type = type;

        _appDbContext.Employees.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
