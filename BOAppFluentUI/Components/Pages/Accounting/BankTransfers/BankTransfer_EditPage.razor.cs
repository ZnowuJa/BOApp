using System.Security.Cryptography;
using System.Text.RegularExpressions;

using Application.CQRS.AccountingCQRS.BusinessPartners.Queries;
using Application.CQRS.AccountingCQRS.BusinessTravels.Queries;
using Application.CQRS.AccountingCQRS.CostCenters.Queries;
using Application.CQRS.AccountingCQRS.GLAccounts.Queries;
using Application.CQRS.AccountingCQRS.SapCostCenters.Queries;
using Application.CQRS.AccountingCQRS.VATRates.Queries;
using Application.CQRS.BusinessOperationsCQRS;
using Application.CQRS.General.ManagerDeputies.Queries;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Currencies.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Forms.Accounting;
using Application.Forms.Accounting.BankTransferSmallClasses;
using Application.Forms.Accounting.BuisnessTravelSmallClasses;
using Application.Forms.Accounting.Enums;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;

using Blazored.FluentValidation;

using Domain.Entities.Accounting;
using Domain.Entities.ITWarehouse;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.Graph.Models.TermStore;
using Microsoft.JSInterop;

namespace BOAppFluentUI.Components.Pages.Accounting.BankTransfers;

public partial class BankTransfer_EditPage : ComponentBase
{
    #region Declarations
    #region General
    [Parameter] public int Id { get; set; }
    private EditContext _editContext;
    private FluentValidationValidator? _fluentValidationValidator;
    private BankTransferFormVm formItem = new BankTransferFormVm();
    private string _srcPage;
    private bool blankPage = true;
    private bool isLoading { get; set; } = true;
    private bool showForm { get; set; } = false;
    private FormUserContext _userContext = new FormUserContext("Accountant", "Technician");
    #endregion
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

    private bool isIsIndividualDisabled = false;
    private bool isSplitPaymentVisible = true;
    private bool isLevel3Required = false;


    #endregion
    #region BusinessPartnersSetup
    DataGridSelectMode BPGridMode = DataGridSelectMode.Single;
    //IEnumerable<BusinessPartnerVm> SelectedItems = _businessPartners.Where(p => p.Selected);
    #endregion
    #region HidingShowingSections
    private bool Show { get; set; }
    private bool ShowInvoice { get; set; }
    private bool ShowTaxes { get; set; }
    private bool ShowBusinessPartners => _businessPartners.Count() > 0 && 
    (formItem.FormType == "Używane" || formItem.FormType == "Polisy" || formItem.FormType == "Administracyjne" || formItem.FormType == "Podatkowe" || formItem.FormType == "Cło" || formItem.FormType == "PCC");
    private bool ShowDuty => formItem.FormType == "Cło" ;
    private bool ShowPccTax => formItem.FormType == "PCC";
    #endregion
    #region Dictionaries
    private IQueryable<EmployeeVm> _employees { get; set; }
    private IEnumerable<CostCenterVm> _costCenters { get; set; }
    private IEnumerable<SapCostCenterVm> _sapCostCenters { get; set; }
    private IEnumerable<GLAccountVm> _glAccounts { get; set; }
    private IEnumerable<VATRateVm> _vatRates { get; set; }
    private IEnumerable<VATRateVm> _selectedSplitPaymentVatRate { get; set; }
    private List<SimpleLocation> simpleLocations { get; set; }
    private List<SimpleDepartment> simpleDepartments { get; set; }
    private List<LocationVm> _locations { get; set; } = new List<LocationVm>();
    private static IQueryable<BusinessPartnerVm> _businessPartners { get; set; }= new List<BusinessPartnerVm>().AsQueryable();
    //private IQueryable<string> _BankTransferTypes = Enum.GetValues(typeof(BillReasons))
    //    .Cast<BillReasons>().Select(c => c.ToString()).AsQueryable();
    private IQueryable<string> FormTypes => BankTransferTypes.All.Select(c => c.Display).AsQueryable();
    private IEnumerable<CurrencyVm> _currencies {get; set; }
    private IEnumerable<CurrencyVm> _selectedCurrency { get; set; }
    private string regexNIP =@"\(^(AT)(U\\d{8}$))|(^(BE)(\\d{10}$))|(^(BG)(\\d{9,10}$))|(^(CY)([0-5|9]\\d{7}[A-Z]$))|(^(CZ)(\\d{8,10})?$)|(^(DE)([1-9]\\d{8}$))|(^(DK)(\\d{8}$))|(^(EE)(10\\d{7}$))|(^(EL)(\\d{9}$))|(^(ES)([0-9A-Z][0-9]{7}[0-9A-Z]$))|(^(EU)(\\d{9}$))|(^(FI)(\\d{8}$))|(^(FR)([0-9A-Z]{2}[0-9]{9}$))|(^(GB)((?:[0-9]{12}|[0-9]{9}|(?:GD|HA)[0-9]{3})$))|(^(GR)(\\d{8,9}$))|(^(HR)(\\d{11}$))|(^(HU)(\\d{8}$))|(^(IE)([0-9A-Z\\*\\+]{7}[A-Z]{1,2}$))|(^(IT)(\\d{11}$))|(^(LV)(\\d{11}$))|(^(LT)(\\d{9}$|\\d{12}$))|(^(LU)(\\d{8}$))|(^(MT)([1-9]\\d{7}$))|(^(NL)(\\d{9}B\\d{2}$))|(^(NO)(\\d{9}$))|(^(PL)(\\d{10}$))|(^(PT)(\\d{9}$))|(^(RO)([1-9]\\d{1,9}$))|(^(RU)(\\d{10}$|\\d{12}$))|(^(RS)(\\d{9}$))|(^(SI)([1-9]\\d{7}$))|(^(SK)([1-9]\\d[(2-4)|(6-9)]\\d{7}$))|(^(SE)(\\d{10}01$))\";
    #endregion
    #region ApproversAndWorkflow
    private OrganisationRoleForFormVm approverL1 { get; set; } = new();
    private OrganisationRoleForFormVm approverL2 { get; set; } = new();
    private OrganisationRoleForFormVm approverL3 { get; set; } = new();
    private OrganisationRoleForFormVm approverL4 { get; set; } = new();
    private OrganisationRoleForFormVm approverL5 { get; set; } = new();
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
    #region Taxes
    private string duties { get; set; } = string.Empty;
    //private List<Duty> convertedDuties { get; set; } = new List<Duty>();
    private IQueryable<Duty> _duties { get; set; } = new List<Duty>().AsQueryable();
    public string pccTaxes { get; set; } = string.Empty;
    //public List<PccTax> convertedPccTaxes { get; set; } = new List<PccTax>();
    public IQueryable<PccTax> _pccTaxes { get; set; } = new List<PccTax>().AsQueryable();
    #endregion
    #region [Files Management]
    FluentInputFile? myFileUploader = default!;
    FluentInputFileEventArgs[] Files = Array.Empty<FluentInputFileEventArgs>();

    int ProgressPercent = 0;
    int? progressPercent;
    string? progressTitle;
    private IQueryable<string> AttTypes { get; set; } = new List<string>{"Faktura","Proforma","Zamówienie","Orzeczenie lekarskie","Inne"}.AsQueryable();
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

    #region SetupForm
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
        IsSplitPaymentDisabled(false);
        await AssignAllApprovers(_userContext.Employee);
        //StateHasChanged();
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
        _locations = tempLocations.ToList();
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
        //_businessPartners = await _mediator.Send(new GetAllBusinessPartnerQuery());
        _employees = await _mediator.Send(new GetAllEmployeesQuery());


    }
    #endregion

    #region Approvers
    private async Task AssignAllApprovers(EmployeeVm emp)
    {
        try
        {
            int lvl1managerId = 0;
            int lvl2managerId = 0;
            int lvl3managerId = 0;

            var empMan = _employees.FirstOrDefault(e => e.EnovaEmpId == emp.ManagerId) ?? new EmployeeVm();
            Console.WriteLine();

            if (emp.EnovaEmpId == 4082)
            {
                lvl1managerId = 104;
                lvl2managerId = 976;
            }
            else
            {
                if (empMan.ManagerId == 0)
                {
                    lvl1managerId = emp.ManagerId;
                    lvl2managerId = 976;
                }
                else
                {
                    lvl1managerId = emp.ManagerId;
                    lvl2managerId = empMan.ManagerId;
                }
            }
            EmployeeVm L2Man = _employees.FirstOrDefault(e => e.EnovaEmpId == lvl2managerId) ?? new EmployeeVm();


            formItem.Level1Approvers.Clear();
            formItem.Level2Approvers.Clear();
            formItem.Level3Approvers.Clear();
            formItem.Level4Approvers.Clear();
            formItem.Level5Approvers.Clear();

            formItem.Level1Approvers = await SetManagerAndDeputies(lvl1managerId); // Przełożony
            approverL1 = formItem.Level1Approvers[0];
            formItem.LVL1_EnovaEmpId = emp.ManagerId.ToString();
            formItem.LVL1_EmployeeName = emp.Manager.LongName;

            formItem.Level2Approvers = await SetManagerAndDeputies(lvl2managerId);
            approverL2 = formItem.Level2Approvers[0];
            formItem.LVL2_EnovaEmpId = L2Man.EnovaEmpId.ToString();
            formItem.LVL2_EmployeeName = L2Man.LongName;// Dyrektor oddziału

            formItem.Level4Approvers = SetApprovers(_organisation.Role_Accountants); // Księgowe
            approverL4 = formItem.Level4Approvers[0];
            formItem.LVL4_EnovaEmpId = approverL4.EmpId.ToString();
            formItem.LVL4_EmployeeName = approverL4.LongName;
            _logger.LogInformation($"BT400 LongName: {_organisation.Id} ");
            _logger.LogInformation($"BT400 LongName: {_organisation.Role_AccountantsTeamLeader.FirstOrDefault().Employee.LongName} ");
            _logger.LogInformation($"BT400 LongName: {_organisation.ToString()} ");
            formItem.Level5Approvers = SetApprovers(_organisation.Role_AccountantsTeamLeader); // KsięgoweTL

            approverL5 = formItem.Level5Approvers.FirstOrDefault();
            formItem.LVL5_EnovaEmpId = approverL5.EmpId.ToString();
            formItem.LVL5_EmployeeName = approverL5.LongName;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);

        }
        
    }
    private List<OrganisationRoleForFormVm> SetApprovers(List<OrganisationRoleVm> rolesIn)
    {
        List<OrganisationRoleForFormVm> result = new();
        foreach (var role in rolesIn)
        {
            result.Add(new OrganisationRoleForFormVm(role));
        }
        // Find the default role
        var defaultRole = result.FirstOrDefault(r => r.IsDefault);

        // If a default role exists, move it to the first position
        if (defaultRole != null)
        {
            result.Remove(defaultRole);
            result.Insert(0, defaultRole);
        }

        return result;
        // rolesIn.Select(role => new OrganisationRoleForFormVm(role)).ToList();
    }
    private async Task<List<OrganisationRoleForFormVm>> SetManagerAndDeputies(int manId)
    {
        var result = new List<OrganisationRoleForFormVm>();
        var manDep = new ManagerDeputyVm();
        var man = _employees.Where(e => e.EnovaEmpId == manId).First();
        OrganisationRoleForFormVm apprL1 = new()
        {
            IsDefault = true,
            EmpId = man.EnovaEmpId,
            LongName = man.LongName
        };
        manDep = await _mediator.Send(new GetManagerDeputyByManagerIdQuery(man.EnovaEmpId));
        if (manDep != null)
        {
            var managerDeputies = manDep.Deputies;

            foreach (var item in manDep.Deputies)
            {
                result.Add(new OrganisationRoleForFormVm(item));
            }
        }

        result.Insert(0, apprL1);

        return result;

    }
    // JOJOJO: sd;fskd;fldsk;fldskf;lsdf
    private async Task HandleChangeApproverL1(ChangeEventArgs e)
    {
        var tempEmp = _employees.First(p => p.EnovaEmpId == int.Parse(e.Value.ToString()));
        formItem.LVL1_EnovaEmpId = tempEmp.EnovaEmpId.ToString();
        formItem.LVL1_EmployeeName = tempEmp.LongName;
        // formItem.LVL1_EmployeeEmail = tempEmp.Email;
    }
    // HACK: sddfsdfsdf
    private async Task HandleChangeApproverL2(ChangeEventArgs e)
    {
        // TODO: Pamiętaj o tym!
        var tempEmp = _employees.First(p => p.EnovaEmpId == int.Parse(e.Value.ToString()));
        formItem.LVL2_EnovaEmpId = tempEmp.EnovaEmpId.ToString();
        formItem.LVL2_EmployeeName = tempEmp.LongName;
        // formItem.LVL1_EmployeeEmail = tempEmp.Email;
    }
    private async Task SetCurrentApproverName(string currentNewStatus)
    {
        // TODO: UPDATE method and align all fields accordingly - this is a acopy from BusinessTrip.

        //Check statuses values and accept or change what you see.
        //use the method only after user finishes work - sending or saving document

        if (currentNewStatus == "AprobataL1")
        {
            formItem.CurrentApproverName = formItem.LVL1_EmployeeName;
        }
        else if (currentNewStatus == "ZaliczkaKasa")
        {
            formItem.CurrentApproverName = $"{_organisation.SapNumber}_Kasa";

        }
        else if (currentNewStatus == "ZaliczkaKsiegowosc")
        {
            formItem.CurrentApproverName = $"{_organisation.SapNumber}_Księgowe";

        }
        else if (currentNewStatus == "ZaliczkaKsiegowoscTL")
        {
            formItem.CurrentApproverName = formItem.Level4Approvers.Where(tl => tl.IsDefault).Select(t => t.LongName).First();

        }
        else if (currentNewStatus == "WyslaneDoRobota")
        {
            formItem.CurrentApproverName = "Robot";

        }
        else if (currentNewStatus == "BladRobota")
        {
            formItem.CurrentApproverName = $"{_organisation.SapNumber}_Księgowe";

        }
        else if (currentNewStatus == "Rozliczenie")
        {
            formItem.CurrentApproverName = formItem.EmployeeName;

        }
        else if (currentNewStatus == "Ksiegowosc")
        {
            formItem.CurrentApproverName = $"{_organisation.SapNumber}_Księgowe";

        }
        else if (currentNewStatus == "KsiegowoscTL")
        {
            formItem.CurrentApproverName = formItem.Level4Approvers[0].LongName;
            // formItem.CurrentApproverName = formItem.Level4Approvers.Where(tl => tl.IsDefault).Select(t => t.LongName).First();
            Console.WriteLine();

        }
        else if (currentNewStatus == "AprobataL11")
        {
            formItem.CurrentApproverName = formItem.LVL1_EmployeeName;

        }
        else if (currentNewStatus == "AprobataL12")
        {
            formItem.CurrentApproverName = formItem.LVL5_EmployeeName;
        }
        else if (currentNewStatus == "KasaRozliczenie")
        {
            // tu poprawić bo kasa może zostać wybrana inna niż domyslna organizacji - może trzeba to zaktualizowac na HandleLocationChange???
            formItem.CurrentApproverName = $"{_organisation.SapNumber}_Kasa";

        }
        else if (currentNewStatus == "WyslaneDoRobotaRozliczenie")
        {
            formItem.CurrentApproverName = "Robot";

        }
        else if (currentNewStatus == "BladRobotaRozliczenie")
        {
            //ewentualnie wyczytać z Approvals kto wysłał do robota.
            formItem.CurrentApproverName = $"{_organisation.SapNumber}_Księgowe";
        }
        else if (currentNewStatus == "Rozliczone")
        {
            formItem.CurrentApproverName = "Minibuchung";
        }
        else if (currentNewStatus == "Zamkniete")
        {
            formItem.CurrentApproverName = "Zamkniete";
        }
    }
    #endregion
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
    private void OnVATRateSearch(OptionsSearchEventArgs<VATRateVm> e)
    {
        if (e.Text != null)
        {
            e.Items = _vatRates.Where(a =>
                a.Percentage.ToString().Contains(e.Text, StringComparison.OrdinalIgnoreCase)).ToList();
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



    private Task HandleBusinessPartnerSelection((BusinessPartnerVm Item, bool Selected) e)
    {
        // Perform the assignment
        e.Item.Selected = e.Selected;
        if (e.Item.Selected)
        {
            formItem.RecipientName = e.Item.Name;
            formItem.RecipientAddressStreet = e.Item.Street;
            formItem.RecipientAddressCity = e.Item.City;
            formItem.RecipientAddressPostCode = e.Item.PostalCode;
            formItem.RecipientAddressCountry = e.Item.Country;
            formItem.RecipientVatId = e.Item.VatId;
            formItem.BankTransferMapping.BankAccountNumber = e.Item.BankAccountNumber;
        } else
        {
            formItem.RecipientName = string.Empty;
            formItem.RecipientAddressStreet = string.Empty;
            formItem.RecipientAddressCity = string.Empty;
            formItem.RecipientAddressPostCode = string.Empty;
            formItem.RecipientAddressCountry = string.Empty;
            formItem.RecipientVatId = string.Empty;
            formItem.BankTransferMapping.BankAccountNumber = string.Empty;
        }
            // Add any additional logic you want to execute
            // For example, logging or updating other components
            Console.WriteLine($"Selected: {e.Item.Name}");

        return Task.CompletedTask;
    }


    private async Task HandleFormTypeChange()
    {
        formItem.RecipientName = formItem.RecipientVatId = formItem.RecipientAddressCity = formItem.RecipientAddressCountry = formItem.RecipientAddressPostCode = formItem.RecipientAddressStreet = formItem.BankTransferMapping.BankAccountNumber = string.Empty;
        formItem.BankTransferMapping.BankTrasferTitle = string.Empty;

        _businessPartners = await _mediator.Send(new GetAllBusinessPartnersByTypeNameQuery(formItem.FormType));
        formItem.IsIndividual = false;
        isIsIndividualDisabled = false;
        isSplitPaymentVisible = true;


        if (formItem.FormType == "Cło")
        {
            formItem.IsIndividual = false;
            isIsIndividualDisabled = true;
            isSplitPaymentVisible = false;
            formItem.BankTransferMapping.BankTrasferTitle = $"Cło z dnia i cośtam do poprawy {DateOnly.FromDateTime(DateTime.Now)}";
        }
        else if (formItem.FormType == "PCC")
        {
            formItem.IsIndividual = false;
            isIsIndividualDisabled = true;
            isSplitPaymentVisible = false;
            formItem.BankTransferMapping.BankTrasferTitle = $"Podatek z dnia {DateOnly.FromDateTime(DateTime.Now)}";
        }

        _editContext.NotifyFieldChanged(FieldIdentifier.Create(() => formItem.Attachments));
    }
    private async Task HandleCurrencyChange()
    {
        formItem.Currency = _selectedCurrency.FirstOrDefault();
        if (formItem.Currency == null)
        {
            formItem.Currency = _currencies.FirstOrDefault(c => c.Name == "PLN");
        }
    }
    private async Task HandleSplitPaymentVatRateChange()
    {
        formItem.SplitPaymentVatRate = _selectedSplitPaymentVatRate.FirstOrDefault();
        if (formItem.SplitPaymentVatRate == null)
        {
            formItem.SplitPaymentVatRate = new VATRateVm();
        }
    }
    private async Task HandleIsIndividualChange()
    {
        if(formItem.IsIndividual)
        {
            formItem.SplitPayment = false;
            IsSplitPaymentDisabled(true);
            isSplitPaymentVisible = false;
        }
        else if (!formItem.IsIndividual)
        {
            formItem.SplitPayment = false;
            formItem.SplitPaymentVatRate = new VATRateVm();
            formItem.SplitPaymentAmount = 0;
            IsSplitPaymentDisabled(false);
            isSplitPaymentVisible = true;
        }
        //Console.WriteLine(formItem.IsIndividual.ToString());
    }
    
    private async Task HandleSplitPaymentAmountChange()
    {
        //Console.WriteLine(formItem.IsIndividual.ToString());
    }

    private async Task HandleDutyChange()
    {
        if (!string.IsNullOrWhiteSpace(duties))
        {
            var rows = duties
                .Trim()
                .Replace("\r\n", "\n")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var parsedDuties = new List<Duty>();

            foreach (var row in rows)
            {
                // Support both tab and comma as delimiters
                var cells = Regex.Split(row.Trim(), @"[\t,]");

                if (cells.Length < 6)
                    continue;

                try
                {
                    var duty = new Duty
                    {
                        ReceiveDate = DateOnly.Parse(cells[0].Trim()),
                        DocumentDate = DateOnly.Parse(cells[1].Trim()),
                        ReferenceNumber = cells[2].Trim(),
                        NetValue = decimal.TryParse(cells[3].Trim(), out var net) ? net : 0m,
                        VatValue = decimal.TryParse(cells[4].Trim(), out var vat) ? vat : 0m,
                        DutyValue = decimal.TryParse(cells[5].Trim(), out var clo) ? clo : 0m
                    };

                    parsedDuties.Add(duty);
                }
                catch
                {
                    // Log error if needed
                }
            }

            //convertedDuties = parsedDuties;
            _duties = parsedDuties.AsQueryable();
            if (_duties.Count() > 0)
            {
                formItem.Amount = _duties.Sum(x => x.DutyValue);
            }
        }
    }
    private async Task HandlePCCTaxChange()
    {
        if (!string.IsNullOrWhiteSpace(pccTaxes))
        {
            var rows = pccTaxes
                .Trim()
                .Replace("\r\n", "\n")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var parsedTaxes = new List<PccTax>();

            foreach (var row in rows)
            {
                // Support both tab and comma as delimiters
                var cells = Regex.Split(row.Trim(), @"[\t,]").ToList();

                //if (cells.Length < 6)
                //    continue;
                while (cells.Count < 16)
                {
                    cells.Add(string.Empty);
                }
                try
                {
                    var pccTax = new PccTax
                    {

                        AgreementNumber = cells[2].Trim(),
                        VIN = cells[6].Trim(),
                        Amount = decimal.TryParse(cells[9].Trim(), out var vat) ? vat : 0m,
                        InvoiceNumber = cells[12].Trim(),
                        LocationNumber = cells[13].Trim(),
                        GLAccount = cells[14].Trim(),
                        DepartmentNumber = cells[15].Trim(),
                    };

                    parsedTaxes.Add(pccTax);
                }
                catch
                {
                    // Log error if needed
                }
            }

            //convertedPccTaxes = parsedTaxes;
            _pccTaxes = parsedTaxes.AsQueryable();
            if (_pccTaxes.Count() > 0)
            {
                formItem.Amount = _pccTaxes.Sum(x => x.Amount);
            }
        }


    }
    #endregion
    #region FileManagement
    private async Task OnCompletedAsync(IEnumerable<FluentInputFileEventArgs> files)
    {
        var currentTime = DateTime.Now;

        foreach (var file in files)
        {
            if (file.LocalFile == null)
                continue;
            var newID = Guid.NewGuid();
            var filePath = Path.Combine(
                Environment.WebRootPath,
                "Attachments",
                "BankTransfers",
                currentTime.ToString("yyyy"),
                currentTime.ToString("yyyyMM"),
                currentTime.ToString("yyyyMMdd"));

            Directory.CreateDirectory(filePath);

            var finalPath = Path.Combine(
                filePath,
                $"{newID}{Path.GetExtension(file.Name)}");

            // Przenosimy plik z folderu tymczasowego
            FileUtils.MoveFile(file.LocalFile.FullName, finalPath);

            // Dodajemy do listy Attachmentów
            formItem.Attachments.Add(new Attachment
            {
                Id = newID,
                OriginalFileName = file.Name,
                FilePath = finalPath,
                AttUrl = FileUtils.GenerateUrl(finalPath, Environment.WebRootPath)
            });
        }

        await Task.Delay(500);
        ProgressPercent = 0;
    }

    private void RemoveAttachment(Attachment attachment)
    {
        FileUtils.RemoveFile(attachment.FilePath);
        formItem.Attachments.Remove(attachment);
    }
    private async Task OpenAttachment(MouseEventArgs e){

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
    private bool IsSplitPaymentDisabled(bool? b = null)
    {
        if (b.HasValue)
        {
            return b.Value;
        }

        return false;
    }

    #endregion

    private async Task CheckForm()
    {
        _editContext.Validate();
    }
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
