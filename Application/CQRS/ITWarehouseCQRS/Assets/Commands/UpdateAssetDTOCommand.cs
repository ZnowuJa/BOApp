using Application.DTOs;
using Application.Interfaces;
using AutoMapper;

using Domain.Entities.ITWarehouse;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Commands;
public class UpdateAssetDTOCommand : IRequest<int>
{
    public UpdateAssetDTOCommand(AssetDTO a)
    {
        item = a;
    }

    public AssetDTO item { get; }
}

public class UpdateAssetDTOCommandHandler : IRequestHandler<UpdateAssetDTOCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateAssetDTOCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }


    public async Task<int> Handle(UpdateAssetDTOCommand request, CancellationToken cancellationToken)
    {
        var source = await _appDbContext.Assets.Where(a => a.Id == request.item.Id).FirstOrDefaultAsync();

        source = _mapper.Map<Asset>(request.item);
        _appDbContext.Assets.Update(source);
        await _appDbContext.SaveChangesAsync();

        Console.WriteLine(source.SaleFormId);
        //var res = new AssetVm();
        return request.item.Id;
    }
}
