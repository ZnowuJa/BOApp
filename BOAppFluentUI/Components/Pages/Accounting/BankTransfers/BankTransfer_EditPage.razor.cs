using System.Security.Cryptography;

using Application.CQRS.AccountingCQRS.BusinessTravels.Queries;
using Application.CQRS.AccountingCQRS.CostCenters.Queries;
using Application.CQRS.AccountingCQRS.GLAccounts.Queries;
using Application.CQRS.AccountingCQRS.SapCostCenters.Queries;
using Application.CQRS.AccountingCQRS.VATRates.Queries;
using Application.CQRS.BusinessOperationsCQRS;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Currencies.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Forms.Accounting;
using Application.Forms.Accounting.BuisnessTravelSmallClasses;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;

using Blazored.FluentValidation;

using Domain.Entities.ITWarehouse;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.Graph.Models.TermStore;
using Microsoft.JSInterop;

namespace BOAppFluentUI.Components.Pages.Accounting.BankTransfers;

public partial class BankTransfer_EditPage : ComponentBase
{
    #region Declarations
    [Parameter] public int Id { get; set; }
    private EditContext _editContext;
    private FluentValidationValidator? _fluentValidationValidator;
    private BankTransferFormVm formItem = new BankTransferFormVm();
    private string _srcPage;
    private bool blankPage = true;
    private bool isLoading { get; set; } = true;
    private bool showForm { get; set; } = false;
    private FormUserContext _userContext = new FormUserContext("Accountant", "Technician");

    #region Steering and Button bools
    private bool strej = false;

    private bool stal1 = false;
    private bool stal2 = false;
    private bool stal3 = false;

    private bool stks = false;
    private bool stkstl = false;
    private bool stwys = false;
    private bool stblad = false;

    private bool strozend = false;
    private bool stzam = false;
    private bool stodrz = false;

    private bool DisableApproveButton = true;
    private bool DisableRejectButton = true;
    private bool DisableSaveButton = true;
    private bool DisableSendButton = true;

    private bool isOdbiorcaVisible = true;
    #endregion
    #region HidingShowingSections
    private bool Show { get; set; }
    private bool ShowInvoice { get; set; }

    private bool ShowTaxes { get; set; }
    #endregion
    #region Dictionaries
    private IEnumerable<CostCenterVm> _costCenters { get; set; }
    private IEnumerable<SapCostCenterVm> _sapCostCenters { get; set; }
    private IEnumerable<GLAccountVm> _glAccounts { get; set; }
    private IEnumerable<VATRateVm> _vatRates { get; set; }
    private List<SimpleLocation> simpleLocations { get; set; }
    private List<SimpleDepartment> simpleDepartments { get; set; }
    private List<LocationVm> _locations { get; set; } = new List<LocationVm>();
    private IQueryable<string> FormTypes = new List<string>
    {
        "Zwrot do korekty","Uzywane","Polisy","Podatkowe","Inne","Zwrot nadplaty","Proforma","Zwrot do TU","Administracyjne","Okulary","Nowe od Dealera","Clo","PCC","CEPIK"
    }.AsQueryable();
    private IEnumerable<CurrencyVm> _currencies {get; set; }
    private IEnumerable<CurrencyVm> _selectedCurrency { get; set; }
    private string regexNIP =@"\(^(AT)(U\\d{8}$))|(^(BE)(\\d{10}$))|(^(BG)(\\d{9,10}$))|(^(CY)([0-5|9]\\d{7}[A-Z]$))|(^(CZ)(\\d{8,10})?$)|(^(DE)([1-9]\\d{8}$))|(^(DK)(\\d{8}$))|(^(EE)(10\\d{7}$))|(^(EL)(\\d{9}$))|(^(ES)([0-9A-Z][0-9]{7}[0-9A-Z]$))|(^(EU)(\\d{9}$))|(^(FI)(\\d{8}$))|(^(FR)([0-9A-Z]{2}[0-9]{9}$))|(^(GB)((?:[0-9]{12}|[0-9]{9}|(?:GD|HA)[0-9]{3})$))|(^(GR)(\\d{8,9}$))|(^(HR)(\\d{11}$))|(^(HU)(\\d{8}$))|(^(IE)([0-9A-Z\\*\\+]{7}[A-Z]{1,2}$))|(^(IT)(\\d{11}$))|(^(LV)(\\d{11}$))|(^(LT)(\\d{9}$|\\d{12}$))|(^(LU)(\\d{8}$))|(^(MT)([1-9]\\d{7}$))|(^(NL)(\\d{9}B\\d{2}$))|(^(NO)(\\d{9}$))|(^(PL)(\\d{10}$))|(^(PT)(\\d{9}$))|(^(RO)([1-9]\\d{1,9}$))|(^(RU)(\\d{10}$|\\d{12}$))|(^(RS)(\\d{9}$))|(^(SI)([1-9]\\d{7}$))|(^(SK)([1-9]\\d[(2-4)|(6-9)]\\d{7}$))|(^(SE)(\\d{10}01$))\";
    #endregion

    #region Styling
    private JustifyContent Justification = JustifyContent.FlexStart;
    private int Spacing = 1;


    #endregion
    #region Others
    private OrganisationVm _organisation { get; set; }
    private ManagerDeputyVm _managerDeputy { get; set; } = new();
    private List<string> allowedEmp { get; set; } = new List<string>();
    private List<string> allowedEditor { get; set; } = new List<string>();
    private ApprovalVm approvalInfo { get; set; }
    private RejectReason lastRejectReason { get; set; }
    #endregion
    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (Id != 0)
        {
            formItem = await _mediator.Send(new GetBankTransfersByIdQuery(Id));
            if (formItem == null)
            {
                showForm = false;
                isLoading = false;
                return;
            }
        }

        _editContext = new EditContext(formItem);

        await Utils.GetUserName(_authenticationStateProvider, _userContext, _mediator);
        await GetDictionaries();
        SetSrcPage();
        AssignStatusBool();
        await GetAllowedPersons();
        approvalInfo = new ApprovalVm()
        {
            Status = formItem.Status,
            EnovaEmpId = _userContext.EnovaEmpId,
            LongName = _userContext.LongName,
            Date = DateTime.Now,
            IsApproved = true
        };
        

        await SetupForm(formItem.Status);
    }
    private async Task SetupForm(string status)
    {
        ResetForm();
        formItem.Currency = _currencies.Where(c => c.Name == "PLN").FirstOrDefault();
        if (strej)
            await SetupFormRejestracja();
        else if (stal1 || stal2 || stal3)
            await SetupFormAprobata(status);
        else if (stwys)
            await SetupFormWyslaneDoRobota();
        else if (stblad)
            await SetupFormBladRobota();
        else if (stks)
            await SetupFormKsiegowosc();
        else if (stkstl)
            await SetupFormKsiegowoscTL();
        else if (stzam)
            await SetupFormZamkniete();

        if (_srcPage == "view")
        { DisableAllControls(); }
    }
    private async Task SetupFormRejestracja()
    {
        formItem.EmployeeName = _userContext.LongName;
        formItem.EnovaEmpId = _userContext.EnovaEmpId;
        formItem.OrganisationSapNumber = _organisation.SapNumber;
        DisableApproveButton = true;
        DisableRejectButton = true;
        DisableSaveButton = false;
        DisableSendButton = false;
        var tempCurrencies = new List<CurrencyVm>();
        tempCurrencies.Add(formItem.Currency);
        _selectedCurrency = tempCurrencies.AsEnumerable();
    }
    private async Task SetupFormAprobata(string status)
    {

    }
    private async Task SetupFormKsiegowosc()
    {

    }
    private async Task SetupFormKsiegowoscTL()
    {

    }
    private async Task SetupFormWyslaneDoRobota()
    {

    }
    private async Task SetupFormBladRobota()
    {
        
    }
    private async Task SetupFormZamkniete()
    {
        
    }
    private void ResetForm() { }
    private void EnableAllControls() { }
    private void DisableAllControls() { }
    private void SetSrcPage()
    {
        var uri = new Uri(_navigationManager.Uri);
        var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

        if (query.TryGetValue("srcPage", out var sourcePageValue))
        {

            if (sourcePageValue == "kierownikL1" || sourcePageValue == "managerL1" || sourcePageValue == "kierownikL11" || sourcePageValue == "managerL11" || sourcePageValue == "kierownikL12" || sourcePageValue == "managerL12" || sourcePageValue == "pracownik" || sourcePageValue == "employee" || sourcePageValue == "kasjer" || sourcePageValue == "cashier" || sourcePageValue == "ksiegowe" || sourcePageValue == "accountants" || sourcePageValue == "view")
            {
                _srcPage = sourcePageValue; // Valid sourcePage is set
            }
            else
            {

                blankPage = false; // Invalid value
                return;
            }
        }
        else
        {
            blankPage = false;
            _srcPage = "view";
            return;
        }
    }
    private void AssignStatusBool()
    {
        strej = formItem.Status == "Rejestracja";
        stal1 = formItem.Status == "AprobataL1";
        stal2 = formItem.Status == "AprobataL2";
        stal3 = formItem.Status == "AprobataL3";
        stwys = formItem.Status == "WyslaneDoRobota";
        stblad = formItem.Status == "BladRobota";
        stks = formItem.Status == "Ksiegowosc";
        stkstl = formItem.Status == "KsiegowoscTL";
        stodrz = formItem.Status == "Odrzucone";
        stzam = formItem.Status == "Zamkniete";
    }
    private async Task GetAllowedPersons()
    {
        if (strej)
        {
            allowedEmp.Add(_userContext.EnovaEmpId);
            allowedEditor.Add(_userContext.EnovaEmpId);
        }
        if (stal1 || stal2)
        {
            allowedEmp.Add(formItem.EnovaEmpId);
            allowedEmp.Add(formItem.LVL1_EnovaEmpId); //this is manager of requestor
            allowedEmp.AddRange(formItem.Level2Approvers.Select(l => l.EmpId.ToString()));//this is Settlement Department
            allowedEditor.AddRange(formItem.Level2Approvers.Select(l => l.EmpId.ToString()));
        }
        else
        {
            allowedEmp.Add(formItem.EnovaEmpId);
            allowedEmp.AddRange(formItem.Level2Approvers.Select(l => l.EmpId.ToString()));
        }

        if (_userContext.isFormAdmin)
        {
            allowedEmp.Add(_userContext.EnovaEmpId);
        }

        showForm = allowedEmp.Contains(_userContext.EnovaEmpId);
        isLoading = false;

    }
    private async Task GetDictionaries()
    {
        //_employees = await _mediator.Send(new GetAllEmployeesQuery());
        //_costCenters = await _mediator.Send(new GetAllCostCenterIEnumQuery());
        _sapCostCenters = await _mediator.Send(new GetAllSapCostCenterIEnumQuery());

        _glAccounts = await _mediator.Send(new GetAllGLAccountIEnumQuery());
        var tempLocations = await _mediator.Send(new GetCashLocationsQuery());
        //_locations = tempLocations.ToList();
        _vatRates = await _mediator.Send(new GetAllVatRateIEnumQuery());
        _organisation = await _mediator.Send(new GetOrganisationByEmpSapNumberQuery(_userContext.Employee.SapNumber));
        //CompanyCarRegistrationNumbers = await _mediator.Send(new GetCompanyCarRegistrationNumbersQuery());
        simpleLocations = _sapCostCenters.GroupBy(s => s.LocationNumber)
            .Select(g => new SimpleLocation {
                SAPLocationNumber = g.Key,
                SAPLocationName = g.First().LocationName
            })
            .Distinct()
            .ToList();
        simpleDepartments = _sapCostCenters
            .Select(s => new SimpleDepartment
            {
                SAPDepartmentNumber = s.DepartmentNumber,
                SAPDepartmentName = s.DepartmentName
            })
            .Distinct()
            .ToList();
        _currencies = await _mediator.Send( new GetAllCurrenciesQuery());
    }


    private string GetNextStatus(BusinessTravelFormVm form)
    {
        string newStatus = string.Empty;
        if (formItem.Status == "Rejestracja")
        {
            newStatus = "AprobataL1";
        }
        else if (formItem.Status == "AprobataL1")
        {
            newStatus = "AprobataL2";
        }
        else if (formItem.Status == "Ksiegowosc")
        {
            newStatus = "WyslaneDoRobota";
        }
        else if (formItem.Status == "WyslaneDoRobota")
        {
            newStatus = "KsiegowoscTL";
        }
        else if (formItem.Status == "KsiegowoscTL")
        {
            newStatus = "Zamkniete";
        }
        else if (formItem.Status == "Odrzucone")
        {
            newStatus = "AprobataL1";
        }
        else if (formItem.Status == "BladRobota")
        {
            newStatus = "WyslaneDoRobota";
        }

        return newStatus;
    }

    #region OnSearchMethods
    private void OnCurrencySearch(OptionsSearchEventArgs<CurrencyVm> e)
    {
        if (e.Text != null)
        {
            e.Items = _currencies.Where(a =>
                a.Name.Contains(e.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
    private void OnLocationSearch(OptionsSearchEventArgs<LocationVm> e)
    {
        if (e.Text != null)
        {
            e.Items = _locations.Where(a =>
                a.DisplayName.Contains(e.Text, StringComparison.OrdinalIgnoreCase) ||
                a.SapNumber.Contains(e.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    #endregion

    #region OnChangeHandlers
    private async Task HandleCurrencyChange()
    {
        formItem.Currency = _selectedCurrency.FirstOrDefault();
        if (formItem.Currency == null)
        {
            formItem.Currency = _currencies.FirstOrDefault(c => c.Name == "PLN");
        }
    }

    private async Task HandleIsIndividualChange()
    {
        //formItem.IsIndividual = value;
        Console.WriteLine(formItem.IsIndividual.ToString());
    }
    #endregion

    #region ControlsEnebleDisable

    private bool IsControlFormTypeDisabled(bool? b = null)
    {
        if (b.HasValue)
        {
            return b.Value;
        }

        //bool result = true;
        //if (strej)
        //{
        //    result = false;
        //}

        return !strej;
    }
    private bool IsControlIndividualDisabled(bool? b = null)
    {
        if (b.HasValue)
        {
            return b.Value;
        }

        return !strej;
    }

    #endregion


    private async Task ViewAttachment(string url)
    {
        // Navigate to the URL in a new tab

        string url2 = _navigationManager.BaseUri + url;
        await JS.InvokeVoidAsync("open", url, "_blank");
    }
    private async Task ConsoleLog(string logMessage)
    {
        await JS.InvokeVoidAsync("logMessage", logMessage);
    }

    private void HandleValidSubmit()
    {

    }
}
