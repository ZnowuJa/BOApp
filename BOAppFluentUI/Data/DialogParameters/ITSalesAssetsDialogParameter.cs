using Application.DTOs;
using Application.Forms;
using Application.ViewModels.General;

using BOAppFluentUI.Components.Pages;

using Microsoft.AspNetCore.Components;

using Persistance.Migrations;

namespace BOAppFluentUI.Data.DialogParameters;

public record ITSalesAssetsDialogParameter
{
    public List<AssetMinimal> AssetsLists { get; set; }
    public List<ITSaleFormVm> Sales { get; set; }
    public int? SelectedSaleId { get; set; }
    public ITSaleFormVm? SelectedSale { get; set; }
    public FormUserContext FormUserContext { get; set; }
    public OrganisationVm OrganisationVm { get; set; }
}
