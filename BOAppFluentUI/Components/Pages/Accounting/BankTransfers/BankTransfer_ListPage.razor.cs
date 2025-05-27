using Application.CQRS.AccountingCQRS.BankTransfers.Queries;
using Application.ExportModels;
using Application.Forms.Accounting;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BOAppFluentUI.Components.Pages.Accounting.BankTransfers;

public partial class BankTransfer_ListPage : ComponentBase
{
    #region Declarations
        [Parameter] public string role { get; set; }
        private IQueryable<BankTransferFormVm> items { get; set; } = Enumerable.Empty<BankTransferFormVm>().AsQueryable();
        private IQueryable<BankTransferFormVm> items2load { get; set; } = Enumerable.Empty<BankTransferFormVm>().AsQueryable();
    
    #region Basic
        private List<FilterColumn<BankTransferFormVm>> filterColumns = new List<FilterColumn<BankTransferFormVm>>
        {
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "ID", Property = c => c.Id.ToString(), IsVisible = false },
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Numer", Property = c => c.Number.ToString()??string.Empty, IsVisible = true },
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Status", Property = c => c.Status.ToString()??string.Empty, IsVisible = true, IsDropdown = true, DropdownValues = StatusesValues, SelectedValues = new List<string> ()}, 
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Powód Odrzucenia.", Property = c => c.RejectReason, IsVisible = false },
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Zgłaszający", Property = c =>(c.EmployeeName.ToString()??string.Empty), IsVisible = false },
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Numer lok.", Property = c => c.OrganisationSapNumber, IsVisible = true },
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Przełożony", Property = c => c.LVL1_EmployeeName, IsVisible = true },
            new FilterColumn<BankTransferFormVm> { FirstRow = true, Label = "Przełożony2", Property = c => c.LVL2_EmployeeName, IsVisible = false }
        };
        
        private static List<string> StatusesValues { get; set; } = new List<string>
        {
            "Rejestracja", "AprobataL1", "AprobataL2", "Zakończone", "Odrzucone"
        };
        private FormUserContext _userContext = new FormUserContext("Accountant", "Technician");
        private int CurrentUserEnowaEmpId { get; set; }
        
        private DebounceDispatcher debounceDispatcher = new DebounceDispatcher();
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        private ToastIntent intent { get; set; }
        private string toastMessage { get; set; } = string.Empty;
        
        private bool actionsDisabled = true;
        private bool viewDisabled = false;
        private bool editDisabled = false;
    #endregion
    
    #endregion

    #region methods
    protected override async Task OnInitializedAsync()
    {

        await Utils.GetUserName(AuthenticationStateProvider, _userContext, _mediator);
        CurrentUserEnowaEmpId = int.Parse(_userContext.EnovaEmpId);
        
        await Load();

    }

    private async Task Load()
    {
        items2load = await _mediator.Send(new GetAllBankTrasferQuery());

        items = role switch
        {
            "pracownik" or "employee" => items2load.Where(x => x.EnovaEmpId == _userContext.EnovaEmpId),
            // "kierownikL1" or "managerL1" => items2load.Where(x => x.Level1Approvers.Any(approver => approver.EmpId == CurrentUserEnowaEmpId) &&  (x.Status == "AprobataL1" || x.Status == "AprobataL2" )),
            "kierownik" or "manager" => items2load.Where(x => x.Level1Approvers.Any(approver => approver.EmpId == CurrentUserEnowaEmpId) && (x.Status == "AprobataL1" || x.Status == "AprobataL11" || x.Status == "AprobataL12")),
            // "kierownikL12" or "managerL12" => items2load.Where(x => x.Level1Approvers.Any(approver => approver.EmpId == CurrentUserEnowaEmpId) && (x.Status == "AprobataL1" || x.Status == "AprobataL2")),
            "kasjer" or "cashier" => items2load.Where(x => x.Level2Approvers.Any(approver => approver.EmpId == CurrentUserEnowaEmpId) && (x.Status == "ZaliczkaKasa" || x.Status == "KasaRozliczenie")),
            "ksiegowe" or "accountants" => items2load,
            "zapisane" or "saved" => items2load.Where(x => x.Level2Approvers.Any(approver => approver.EmpId == CurrentUserEnowaEmpId)),
            null or _ => items2load.Where(x => x.EnovaEmpId == _userContext.EnovaEmpId)
        };

        if (_userContext.isFormAdmin)
        {
            actionsDisabled = false;
        }

    }


    private void HandleClearFilter(ref string filter) => filter = string.IsNullOrWhiteSpace(filter) ? string.Empty : filter;

    private async Task Edit(BankTransferFormVm item = null)
    {
        _navigationManager.NavigateTo($"/przelew/{item.Id}?srcPage={role}");
    }
    private async Task View(BankTransferFormVm item = null)
    {
        _navigationManager.NavigateTo($"/przelew/{item.Id}?srcPage=view");
    }
    private async Task Delete(BankTransferFormVm item = null)
    {
        var dialog = await _dialogService.ShowConfirmationAsync("Kasowanie Polecenia przelewu: " + item.Number + "\n Czy na pewno?", "TAK", "NIE", "Potwierdź operację kasowania");
        var result = await dialog.Result;
        bool deleteConfirm = result.Cancelled;

        ToastIntent intent;
        string message = string.Empty;

        if (deleteConfirm)
        {
            intent = ToastIntent.Error;
            message = $"Nie skasowano Polecenia Przelewu {item.Number}";

        }
        else
        {
            // var i = await _mediator.Send(new DeleteBusinessTripCommand(item));
            intent = ToastIntent.Success;
            message = $"Skasowano Polecenie Przelewu {item.Number}";

        }

        _toastService.ShowToast(intent, message, 3000);
        await Load();
        StateHasChanged();
    }
    private async Task DownloadCsv()
    {
        var selectedItems = @Utils.ApplyFilters(items, filterColumns);
        IQueryable<BankTransferExportModel> exportModels = selectedItems.ProjectTo<BankTransferFormVm, BankTransferExportModel>(_mapper);

        var csvContent = Utils.GenerateCsvPL(exportModels);
        var base64csv = Convert.ToBase64String(csvContent);

        string date = DateTime.Now.ToString("yyyyMMdd");
        string filename = $"{date}_lista.csv";

        // Invoke the JavaScript method to download the CSV
        await JS.InvokeVoidAsync("downloadFilePL", filename, base64csv);
    }

    #endregion 
}