using Application.DTOs;
using Application.Forms.IT;
using Application.ViewModels.General;

using BOAppFluentUI.Components.Pages;

namespace BOAppFluentUI.Data.DialogParameters;

public record ITSalesAssetsDialogParameter
{
    public List<AssetDTO> AssetsLists { get; set; }
    public List<ITSaleFormVm> Sales { get; set; }
    public int? SelectedSaleId { get; set; }
    public ITSaleFormVm? SelectedSale { get; set; }
    public FormUserContext FormUserContext { get; set; }
    public OrganisationVm OrganisationVm { get; set; }
}
