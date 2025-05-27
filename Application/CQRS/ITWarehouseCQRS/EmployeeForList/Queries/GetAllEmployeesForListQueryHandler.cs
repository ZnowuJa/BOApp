using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Employees;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesForListQueryHandler : IRequestHandler<GetAllEmployeesForListQuery, IQueryable<EmployeeForListVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllEmployeesForListQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<EmployeeForListVm>> Handle(GetAllEmployeesForListQuery request, CancellationToken cancellationToken)
    {
        List<EmployeeForListVm> itemList = new List<EmployeeForListVm>();

        var result = await _appDbContext.Employees.Where(p => p.StatusId == 1)
            .Include(i => i.Type)
            .ToListAsync(cancellationToken);
        foreach (var item in result)
        {
            var managerModel = await _appDbContext.Employees.Where(p => p.Id == item.ManagerId).FirstOrDefaultAsync();
            var empTypeVm = new EmployeeTypeVm()
            {
                Id = item.Type.Id,
                Name = item.Type.Name
            };
            //var manager = new ManagerVm()
            //{
            //    Id = managerModel.Id,
            //    FirstName = managerModel.FirstName,
            //    LastName = managerModel.LastName,
            //    LongName = managerModel.FirstName + " " + managerModel.LastName,
            //    Email = managerModel.Email,
            //    MobileNumber = managerModel.MobileNumber,
            //    PhoneNumber = managerModel.PhoneNumber
            //};

            var resultVm = new EmployeeForListVm()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                IdentityUserId = item.IdentityUserId,
                IdentityUserName = item.IdentityUserName,
                MobileNumber = item.MobileNumber,
                PhoneNumber = item.PhoneNumber,
                ManagerId = item.ManagerId,
                EmployeeTypeVm = empTypeVm,
                DG = item.DG,
                CC = item.CC

            };
            itemList.Add(resultVm);
        }
        //itemList = _mapper.Map<EmployeeForListVm>(result);

        return itemList.AsQueryable();
    }
}