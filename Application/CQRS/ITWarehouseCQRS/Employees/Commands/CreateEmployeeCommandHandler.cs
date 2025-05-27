using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateEmployeeCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var type = await _appDbContext.EmployeeTypes.Where(p => p.Id == request.Type.Id).FirstOrDefaultAsync();

        Employee Employee = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            AzureObjectId = request.AzureObjectId,
            IdentityUserName    = request.IdentityUserName,
            ThirdPartyId = request.ThirdPartyId,
            MobileNumber = request.MobileNumber,
            PhoneNumber = request.PhoneNumber,
            ManagerId = request.ManagerId,
            IsManager = request.IsManager,
            Type = type,

        };
        _appDbContext.Employees.Add(Employee);
        await _appDbContext.SaveChangesAsync();
        return Employee.Id;
    }
}
