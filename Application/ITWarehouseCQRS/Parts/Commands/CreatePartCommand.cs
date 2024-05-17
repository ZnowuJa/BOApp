using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Parts.Commands;
public class CreatePartCommand : IRequest<int>
{
    public string Name { get; set; }
    public CategoryVm CategoryVm { get; set; }
    public VendorVm VendorVm { get; set; }
    public string Description { get; set; }
    public string Photo { get; set; }
    public int WarrantyPeriod { get; set; }
    public bool isCurrentlyOrderable { get; set; }
    public DateTime? EndOfSupport { get; set; }

    public CreatePartCommand(string name, CategoryVm categoryVm, VendorVm vendorVm, string description, string photo, int warrantyPeriod, bool iscurrentlyOrderable, DateTime? endOfSupport)
    {
        Name = name;
        CategoryVm = categoryVm;
        VendorVm = vendorVm;
        Description = description;
        Photo = photo;
        WarrantyPeriod = warrantyPeriod;
        isCurrentlyOrderable = iscurrentlyOrderable;
        EndOfSupport = endOfSupport;
    }
}