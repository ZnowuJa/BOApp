using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetEmployeeTypeQueryHandler : IRequestHandler<GetEmployeeTypeQuery, EmployeeTypeVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetEmployeeTypeQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<EmployeeTypeVm> Handle(GetEmployeeTypeQuery request, CancellationToken cancellationToken)
    {
        var employeetype = await _appDbContext.EmployeeTypes.Where(p => p.Id == request.EmployeeTypeId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var employeetypeVM = _mapper.Map<EmployeeTypeVm>(employeetype);
        return employeetypeVM;
    }
}
