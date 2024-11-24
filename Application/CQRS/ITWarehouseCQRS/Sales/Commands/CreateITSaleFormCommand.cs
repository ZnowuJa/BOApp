using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.ITForms;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Sales.Commands;
public class CreateITSaleFormCommand : IRequest<ITSaleFormVm>
{
    public ITSaleFormVm Form { get; set; }

    public CreateITSaleFormCommand(ITSaleFormVm form)
    {
        Form = form;
    }
}

public class CreateITSaleFormCommandHandler : IRequestHandler<CreateITSaleFormCommand, ITSaleFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateITSaleFormCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _appDbContext = context;
        _mapper = mapper;
    }

    public async Task<ITSaleFormVm> Handle(CreateITSaleFormCommand command, CancellationToken cancellationToken)
    {
        var employees = _appDbContext.Employees.Where(e => e.IsActive == 1);
        var companies = _appDbContext.Companies.Where(c => c.StatusId == 1);
        var assets = _appDbContext.Assets.Where(a => a.StatusId == 1);
        
        //var form = new ITSaleForm();
        //try
        //{
        //    form = _mapper.Map<ITSaleForm>(command.Form);
        //    Console.WriteLine();

        //} catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}
        var form = _mapper.Map<ITSaleForm>(command.Form);



        _appDbContext.ITSaleForms.Add(form);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        //foreach (var asset in form.Assets)
        //{
        //    var existingAsset = _appDbContext.Assets.Local.FirstOrDefault(e => e.Id == asset.Id);
        //    if (existingAsset != null)
        //    {
        //        _appDbContext.Entry(existingAsset).State = EntityState.Detached;
        //    }
        //}


        command.Form.Id = form.Id;
        form.Number = command.Form.Number = $"{command.Form.NumberPrefix}{command.Form.Id.ToString("D6")}";
        //form = _mapper.Map<ITSaleForm>(command.Form);
        _appDbContext.ITSaleForms.Update(form);
        await _appDbContext.SaveChangesAsync();

        return command.Form;
    }

}
