using System.Globalization;
using Application.CQRS.AccountingCQRS.CostCenters.Queries;
using Application.CQRS.AccountingCQRS.Countries.Queries;
using Application.CQRS.AccountingCQRS.Dictionaries;
using Application.CQRS.AccountingCQRS.GLAccounts.Queries;
using Application.CQRS.AccountingCQRS.VATRates.Queries;
using Application.CQRS.BusinessOperationsCQRS;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Forms.Accounting;
using Application.Forms.Accounting.Enums;
using Application.ViewModels.Accounting;
using Application.ViewModels.BusinessOperations;
using BOAppFluentUI.Components.Pages;
using BOAppFluentUI.Components.Pages.Accounting.SharedComponents;
using Application.ViewModels.General;
using Microsoft.AspNetCore.Components;
using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MediatR;
using Microsoft.JSInterop;

namespace BOAppFluentUI.Components.Pages.Accounting.BusinessTrips;

public partial class BTSplit : ComponentBase    
{
    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private IMediator Mediator { get; set; }
    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private ILogger<BTSplit> Logger { get; set; }
    [Inject] private IToastService ToastService { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IWebHostEnvironment Environment { get; set; }

    #region Declarations

    [Parameter] public int Id { get; set; }

    # region General
    // private EditContext _editContext;
    private BusinessTravelFormVm formItem = new BusinessTravelFormVm();
    private FormUserContext _userContext = new FormUserContext("BusinessTrip", "Technician");
    private OrganisationRoleForFormVm approverL1 { get; set; }
    private OrganisationRoleForFormVm approverL2 { get; set; }
    private OrganisationRoleForFormVm approverL3 { get; set; }
    private OrganisationRoleForFormVm approverL4 { get; set; }
    private OrganisationRoleForFormVm approverL5 { get; set; }
    private bool al1 = false;
    private bool al2 = false;
    private bool al3 = false;
    private bool al4 = false;
    private bool al5 = false;
    private bool ApproveButton = true;
    private bool SendButton = true;
    private OrganisationVm _organisation { get; set; }
    private WorkflowTemplateVm wf { get; set; }
    private string FormName = "BusinessTravelFormVm";
    private bool local = true;

    private string Title { get; set; } = string.Empty;

    private IQueryable<EmployeeVm> Employees { get; set; }
    private IEnumerable<EmployeeVm> SelectedEmpl { get; set; }
    private IQueryable<string> _Statuses = Enum.GetValues(typeof(Statuses))
        .Cast<Statuses>().Select(c => c.ToString()).AsQueryable();

    #endregion
    #region Time

    private DateTime SelectedStartDate = DateTime.Now;
    private DateTime SelectedEndDate = DateTime.Now;
    private DateOnly? SelectedDateFrom { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private TimeOnly? SelectedTimeFrom { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    private DateTime? SelectedDateTimeFrom { get; set; }

    private DateOnly? SelectedDateTo { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private TimeOnly? SelectedTimeTo { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    private DateTime? SelectedDateTimeTo { get; set; }
    private string DateTimeValidationMessage { get; set; } = string.Empty;
    private string StagesDatesErrorMessage { get; set; } = string.Empty;
    private List<string> StagesDatesErrorMessages { get; set; } = new();

    #endregion Time
    #region Finance

    public string AdvancePaymentCashString
    {
        get => formItem.AdvancePaymentCash.HasValue ? (formItem.AdvancePaymentCash.Value ? "1" : "0") : null;
        set => formItem.AdvancePaymentCash = value switch
        {
            "1" => true,
            "0" => false,
            _ => (bool?)null
        };
    }
    private List<Option<int>> AdvancePaymentMethodsInt = new()
        {
            { new Option<int> { Value = 0, Text = "Wypłata w Kasie", Selected = true } },
            { new Option<int> { Value = 1, Text = "Wypłata przelewem" } },
        };

    private Bill CurrentBillForUpload { get; set; } = new Bill();
    private IQueryable<string> _billReasons = Enum.GetValues(typeof(BillReasons))
        .Cast<BillReasons>().Select(c => c.ToString()).AsQueryable();
    private IEnumerable<CountryVm> SelectedCountry { get; set; }
    private IQueryable<CountryVm> Countries { get; set; }
    private CountryVm Origin { get; set; }
    private IEnumerable<Location> _locations { get; set; } = new List<Location>().AsQueryable();
    IEnumerable<Location> _selectedLocation = Array.Empty<Location>();

    #endregion
    #region DestinationsObjectivesMeans
    private List<CountryVm> SelectedCountries { get; set; } = new();
    private string SelectedCity { get; set; } = string.Empty;
    private IQueryable<string> _cities = Enum.GetValues(typeof(Cities))
        .Cast<Cities>().Select(c => c.ToString()).AsQueryable();
    private IEnumerable<string> SelectedObjective { get; set; }
    private IQueryable<string> _objectives = Enum.GetValues(typeof(Objectives))
        .Cast<Objectives>().Select(c => c.ToString()).AsQueryable();
    private IQueryable<string> Conveyances = new List<string>
        {
            "Samochód służbowy",
            "Samochód prywatny",
            "Transport publiczny"
        }.AsQueryable();
    private IEnumerable<string> SelectedConveyance { get; set; }
    private string SelectedConveyanceTxt { get; set; }
    private bool IsPrivateCar { get; set; } = false;
    private bool IsCompanyCar { get; set; } = false;
    private bool IsPublicTransport { get; set; } = false;
    private IEnumerable<string> SelectedCompanyCar { get; set; }

    #endregion
    #region SteeringBools
    private bool ShowStages { get; set; } = false;
    private bool ShowAccomodations { get; set; } = false;
    private bool ShowMeals { get; set; } = false;
    private bool ShowLocalTravels { get; set; } = false;
    private bool ShowTransits { get; set; } = false;
    private bool ShowCashier { get; set; } = false;
    private bool ShowBills { get; set; } = false;
    private bool ShowSummary { get; set; } = false;
    private bool EnterInvoiceMapping { get; set; } = false;
    #endregion
    #region EnableBools

    private bool IsAdvancedPaymentDisabled { get; set; } = false; //wyłącza kontrolki do zaliczki
    private bool IsCashPayoutNumberDisabled { get; set; } = false; //wyłącza kontrolki do zaliczki
    private bool IsDateTimeDisabled { get; set; } = false; //wyłącza możliwość zmiany dat
    private bool IsDestinationDisabled { get; set; } = true; //wyłącza możliwość zmiany celu podróży
    private bool IsEmployeeDisabled { get; set; } = true; //wyłącza możliwość zmiany delegowanego
    private bool IsTransportationDisabled { get; set; } = true; //wyłącza sekcję transportową
    private bool IsMileageDisabled { get; set; } = true;
    private bool IsBillsDisabled { get; set; } = true; //wyłącza możliwość zmiany wprowadzonych rachunków
    private bool IsInvoiceMappingDisabled { get; set; } = true; //wyłącza możliwość edycji dekretacji faktury

    #endregion
    #region Dictionaries
    private IQueryable<NbpCurrencyRateVm> NbpCurrencies { get; set; }
    private IEnumerable<CompanyCarRegistrationNumberVm> CompanyCarRegistrationNumbers { get; set; }
    private NbpCurrencyRateVm AbroadCurrencyExchangeRate { get; set; }
    private NbpCurrencyRateVm OriginExchangeRate { get; set; } = new NbpCurrencyRateVm(
                        Id: 0,
                        Currency: "Polski Złoty",
                        Code: "PLN",
                        Mid: 1m,
                        RateDate: DateOnly.FromDateTime(DateTime.Today.AddDays(-1))
                        );
    #endregion
    # endregion

    protected override async Task OnInitializedAsync()
    {
        // _editContext = new EditContext(formItem);
        try
        {
            formItem.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            ;
            formItem.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0).AddHours(1);
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"An error occurred during initialization: {ex.Message}");
        }

        await Utils.GetUserName(AuthenticationStateProvider, _userContext, _mediator);

        Console.WriteLine();
        Countries = await _mediator.Send(new GetAllCountryQuery());
        SelectedCountries = new List<CountryVm> { Countries.FirstOrDefault(c => c.Name == "Polska") };
        SelectedCountry = SelectedCountries;
        Origin = SelectedCountries[0];
        NbpCurrencies = await _mediator.Send(new GetNbpCurrencyRatesByDateQuery(DateOnly.FromDateTime(DateTime.Today.AddDays(-1))));
        CompanyCarRegistrationNumbers = await _mediator.Send(new GetCompanyCarRegistrationNumbersQuery());

        Employees = await _mediator.Send(new GetAllEmployeesQuery());
        _locations = await _mediator.Send(new GetLocationsQuery());

        if (SelectedCountry != null && SelectedCountry.Any())
        {
            await AddStage(SelectedCountry.First());
            var firststage = formItem.Stages[0];
            formItem.DestinationCountry = SelectedCountry.FirstOrDefault();
            RecalculateStages(firststage.Id, nameof(firststage.EndDate));
            RecalculateAccomodations();
            RecalculateMeals();
        }
        else
        {
            // Handle the case where SelectedCountry is null or empty
        }

        if (formItem.Status == "Rejestracja")
        {
            SetupFormRejestracja();
        }
        Console.WriteLine();
    }
    private async Task AddStage(CountryVm country)
    {
        AbroadCurrencyExchangeRate = await _mediator.Send(new GetNbpCurrencyRateByDateAndCodeQuery(DateOnly.FromDateTime(DateTime.Today.AddDays(-1)), country.CurrencyVmName));
        if (AbroadCurrencyExchangeRate == null)
            AbroadCurrencyExchangeRate = OriginExchangeRate;


        var duration = (formItem.EndDate.Value - formItem.StartDate.Value).TotalDays;
        var firstStage = new Stage()
        {
            Id = formItem.Stages.Count() + 1,
            CountryCode = country.CountryCode,
            CountryName = country.Name,
            CountryAllowance = country.Allowance,
            CountryCurrency = country.CurrencyVmName,
            StartDate = formItem.StartDate,
            EndDate = formItem.EndDate,
            Duration = Math.Round((decimal)duration, 2)
        };
        formItem.Stages.Add(firstStage);
        ValidateDates();

        var firstAccomodation = new Accommodation()
        {

            StageId = firstStage.Id,
            CountryCode = country.CountryCode,
            CountryName = country.Name,
            Duration = firstStage.Duration,
            AllowanceRate = country.AccomodationAllowance,
            AllowanceRateCurrency = country.CurrencyVmName,
            MaxHotelCost = country.MaxHotelCost,
            HasInvoices = true,
            InvoicesAmount = 0,
            Total = 0
        };
        // var firstAccomodation = new Accommodation()
        //     {

        //         StageId = firstStage.Id,
        //         CountryCode = country.CountryCode,
        //         CountryName = country.Name,
        //         Duration = firstStage.Duration,
        //         AllowanceRate = country.AccomodationAllowance,
        //         AllowanceRateCurrency = country.CurrencyVmName,
        //         HasInvoices = false,
        //         InvoicesAmount = 0,
        //         Total = country.AccomodationAllowance * firstStage.Duration
        //     };
        formItem.Accommodations.Add(firstAccomodation);

        var firstMeal = new Meals()
        {
            StageId = firstStage.Id,
            CountryCode = country.CountryCode,
            CountryName = country.Name,
            AllowanceRate = country.Allowance,
            AllowanceRateCurrency = country.CurrencyVmName,
            Duration = (int)Math.Ceiling(firstStage.AllowanceOrigin.GetValueOrDefault() + firstStage.AllowanceAbroad.GetValueOrDefault())
        };
        formItem.Meals.Add(firstMeal);

        var firstLocalTravel = new LocalTravel()
        {
            StageId = firstStage.Id,
            CountryCode = country.CountryCode,
            CountryName = country.Name,
            AllowanceRate = country.LocalTravelAllowance,
            AllowanceRateCurrency = country.CurrencyVmName,
            Duration = (int)Math.Ceiling(firstStage.AllowanceOrigin.GetValueOrDefault() + firstStage.AllowanceAbroad.GetValueOrDefault())
        };
        formItem.LocalTravels.Add(firstLocalTravel);

        if (firstStage.CountryCode != "PL")
        {
            formItem.Transit = new Transit()
            {
                StageId = firstStage.Id,
                CountryCode = country.CountryCode,
                CountryName = country.Name,
                AllowanceRate = country.TravelAllowance,
                AllowanceRateCurrency = country.CurrencyVmName,
            };
            await RecalculateTransit();
        }

        await RecalculateStages(firstStage.Id, nameof(firstStage.EndDate));
        await RecalculateAccomodations();
        await RecalculateMeals();
        await RecalculateLocalTravels();
        await RecalculateTransit();
        StateHasChanged();
        ConsoleLog($"Country Currency");
    }

    #region OnSearch
    private void OnLocationSearch(OptionsSearchEventArgs<Location> e)
    {
        if (e.Text != null)
        {
            e.Items = _locations.Where(a =>
                a.Name.Contains(e.Text, StringComparison.OrdinalIgnoreCase) ||
                a.SapNumber.Contains(e.Text, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
    private void OnCountrySearch(OptionsSearchEventArgs<CountryVm> e)
    {
        if (e.Text != null)
        {
            e.Items = Countries.Where(a => a.Name.Contains(e.Text, StringComparison.OrdinalIgnoreCase));
        }
    }
    private void OnObjectiveSearch(OptionsSearchEventArgs<string> e)
    {
        if (e.Text != null)
        {
            e.Items = _objectives.Where(a => a.Contains(e.Text, StringComparison.OrdinalIgnoreCase));
        }
    }
    private void OnCompanyCarSearch(OptionsSearchEventArgs<string> e)
    {
        if (e.Text != null)
        {
            e.Items = CompanyCarRegistrationNumbers
                .Where(a => a.RegistrationNumber.Contains(e.Text, StringComparison.OrdinalIgnoreCase))
                .Select(a => a.RegistrationNumber)
                .ToList();
        }
    }
    private void OnAssigneeSearch(OptionsSearchEventArgs<EmployeeVm> e)
    {
        if (e.Text != null)
        {
            e.Items = Employees.Where(a => a.LongName.Contains(e.Text, StringComparison.OrdinalIgnoreCase));
        }
    }
    #endregion OnSearch
    #region OnChange
    private void HandleStatusChange()
    {
        SetupForm(formItem.Status);
    }
    private void HandleBillChange(Bill bill, string curr)
    {
        bill.Currency = curr;
    }
    private void HandlePrivateVehicleMileageChanged(int value)
    {
        formItem.PrivateVehicleMilage = value;

        if (formItem.PrivateVehicleEngineSize <= 900)
        {
            formItem.SumPrivateVehicleAllowance = formItem.PrivateVehicleMilage * Dictionary.MileageAllowance[900];
        }
        else if (formItem.PrivateVehicleEngineSize > 900)
        {
            formItem.SumPrivateVehicleAllowance = formItem.PrivateVehicleMilage * Dictionary.MileageAllowance[901];
        }

    }
    private void HandlePrivateVehicleEngineSizeChanged(int value)
    {

        formItem.PrivateVehicleEngineSize = value;

        if (formItem.PrivateVehicleEngineSize <= 900)
        {
            formItem.SumPrivateVehicleAllowance = formItem.PrivateVehicleMilage * Dictionary.MileageAllowance[900];
        }
        else if (formItem.PrivateVehicleEngineSize > 900)
        {
            formItem.SumPrivateVehicleAllowance = formItem.PrivateVehicleMilage * Dictionary.MileageAllowance[901];
        }

    }
    private void HandleTransitDirectionsChanged(int value)
    {
        formItem.Transit.Directions = value;
        formItem.Transit.Total = formItem.Transit.Directions switch
        {
            0 => 0,
            1 => formItem.Transit.AllowanceRate,
            2 => formItem.Transit.AllowanceRate * 2,
            _ => formItem.Transit.Total
        };
    }
    private void HandleLocationChange()
    {
        formItem.CashPoint = _selectedLocation.FirstOrDefault();

        Console.WriteLine();
    }
    private async Task HandleBillChanged(Bill bill, decimal v)
    {
        bill.Amount = v;
    }
    private async Task HandleLocalTravelChanged(LocalTravel l, int v)
    {
        l.Days = v;
        RecalculateLocalTravels();
    }
    private async Task HandleMealChanged(Meals m, int v, string p)
    {
        var property = m.GetType().GetProperty(p);
        if (property != null && property.CanWrite)
        {
            property.SetValue(m, v);
        }

        RecalculateMeals();
    }
    private async Task HandleStageIncluded(Stage stage, bool value)
    {
        stage.Included = value;
        formItem.Accommodations[stage.Id - 1].Included = value;
        formItem.Meals[stage.Id - 1].Included = value;

        RecalculateAccomodations();
    }
    private async Task HandleAccommodationInvoicesAmount(Accommodation accommodation, decimal? value)
    {
        accommodation.InvoicesAmount = accommodation.Total = value;
    }
    private async Task HandleAccommodationHasInvoices(Accommodation accommodation, bool value)
    {
        accommodation.InvoicesAmount = 0;
        if (value)
        {
            accommodation.HasInvoices = value;
            accommodation.InvoicesAmount = 0;
            accommodation.Total = 0;
        }
        else
        {
            accommodation.HasInvoices = value;
            // accommodation.Total = accommodation.AllowanceRate * accommodation.Duration;
        }
        RecalculateAccomodations();
    }
    private async Task HandleCountryChange()
    {
        Console.WriteLine(SelectedCountry.Count());

        if (SelectedCountry.FirstOrDefault() == null)
        {

        }
        else
        {
            // formItem.Countries = SelectedCountry;
            //
            // formItem.Countries = SelectedEmpl.FirstOrDefault();
            // Role.EmpId = SelectedEmpl.FirstOrDefault().EnovaEmpId;
        }

        StateHasChanged();
    }
    private async Task HandleCountryAdd()
    {
        var newCountry = SelectedCountry.FirstOrDefault();
        // AbroadCurrencyExchangeRate = await _mediator.Send(new GetNbpCurrencyRateByDateAndCodeQuery(DateOnly.FromDateTime(DateTime.Today.AddDays(-1)), newCountry.CurrencyVmName));
        if (newCountry == null)
        {
            newCountry = Countries.FirstOrDefault(c => c.Name == "Polska");
            AbroadCurrencyExchangeRate = OriginExchangeRate;
        }
        else
        {
            AbroadCurrencyExchangeRate = await _mediator.Send(new GetNbpCurrencyRateByDateAndCodeQuery(DateOnly.FromDateTime(DateTime.Today.AddDays(-1)), newCountry.CurrencyVmName));
        }

        formItem.DestinationCountry = newCountry;
        formItem.DestinationCountryCurrency = newCountry.CurrencyVmName;
        // Console.WriteLine(SelectedCountry.Count());1
        if (newCountry != null)
        {
            // Check if the new country is already in the list
            if (!SelectedCountries.Any(c => c.Name == newCountry.Name))
            {
                if (SelectedCountries.Count < 2)
                {
                    // Initially add the first country
                    SelectedCountries.Add(newCountry);
                    AddStage(newCountry);
                    AddStage(Origin);
                    formItem.CurrencyExchamngeRate = AbroadCurrencyExchangeRate.Mid;
                    formItem.CurrencyExchangeRateDate = AbroadCurrencyExchangeRate.RateDate;
                    formItem.DestinationCountryCurrency = AbroadCurrencyExchangeRate.Code;


                }
                else
                {
                    // Update the second country
                    SelectedCountries[1] = newCountry;
                    formItem.Stages[1].CountryCode = newCountry.CountryCode;
                    formItem.Stages[1].CountryName = newCountry.Name;
                }
            }
            else if (SelectedCountries.Count >= 2 && SelectedCountries[1].Name != newCountry.Name)
            {
                if (formItem.Stages.Count > 2)
                {
                    var originStage = formItem.Stages[2]; // Get the stage at index 2
                    formItem.Stages.Remove(originStage); // Remove it
                    var originAccomodation = formItem.Accommodations[2];
                    formItem.Accommodations.Remove(originAccomodation);
                    var originMeal = formItem.Meals[2];
                    formItem.Meals.Remove(originMeal);
                    var originLocalTravel = formItem.LocalTravels[2];
                    formItem.LocalTravels.Remove(originLocalTravel);
                }

                // Update the second country if it's different from the current second country
                SelectedCountries.Remove(SelectedCountries[1]);
                var stageToRemove = formItem.Stages[1];
                formItem.Stages.Remove(stageToRemove);
                var accomodationToRemove = formItem.Accommodations[1];
                formItem.Accommodations.Remove(accomodationToRemove);
                var mealToRemove = formItem.Meals[1];
                formItem.Meals.Remove(mealToRemove);
                var localTravelToRemove = formItem.LocalTravels[1];
                formItem.LocalTravels.Remove(localTravelToRemove);
                formItem.CurrencyExchamngeRate = OriginExchangeRate.Mid;
                formItem.CurrencyExchangeRateDate = OriginExchangeRate.RateDate;
                formItem.DestinationCountryCurrency = OriginExchangeRate.Code;
                ValidateDates();
            }
        }

        StateHasChanged();
    }


    private async Task HandleObjectiveChange()
    {

        Console.WriteLine(SelectedObjective.Count());

        if (SelectedObjective.FirstOrDefault() == null)
        {

        }
        else
        {
            formItem.Objective = SelectedObjective.ToString();
        }

        StateHasChanged();
    }
    private async Task HandleCompanyCarChange()
    {

        Console.WriteLine(SelectedCompanyCar.Count());

        if (SelectedCompanyCar.FirstOrDefault() == null)
        {

        }
        else
        {
            formItem.CompanyVehicleNumber = SelectedCompanyCar.ToString();
        }

        StateHasChanged();
    }
    private async Task HandleCityChange(string value)
    {
        Console.WriteLine();
        formItem.Destination = value;

        StateHasChanged();
    } // Currently not in use.
    private async Task HandleAssigneeChange()
    {
        Console.WriteLine(SelectedEmpl.Count());

        if (SelectedEmpl.FirstOrDefault() == null)
        {

        }
        else
        {
            var emp = SelectedEmpl.FirstOrDefault();
            formItem.EmployeeName = emp.LongName;
            formItem.EnovaEmpId = emp.EnovaEmpId.ToString();
            _organisation = await _mediator.Send(new GetOrganisationByEmpSapNumberQuery(emp.SapNumber));

        }

        StateHasChanged();
    }
    private async Task HandleConveyanceChange()
    {
        Console.WriteLine(SelectedConveyance.Count());

        if (SelectedConveyance.FirstOrDefault() == "Samochód prywatny")
        {
            IsPrivateCar = true;
            IsCompanyCar = false;
            IsPublicTransport = false;
            ShowTransits = false;
        }
        else if (SelectedConveyance.FirstOrDefault() == "Samochód służbowy")
        {
            IsPrivateCar = false;
            IsCompanyCar = true;
            IsPublicTransport = false;
            ShowTransits = false;
        }
        else if (SelectedConveyance.FirstOrDefault() == "Transport publiczny")
        {
            IsPrivateCar = false;
            IsCompanyCar = false;
            IsPublicTransport = true;
            ShowTransits = true;
        }

        StateHasChanged();
    }
    private async Task HandleConveyanceStringChange()
    {
        // Console.WriteLine(SelectedConveyanceTxt.Count());
        if (SelectedConveyanceTxt == "Samochód prywatny")
        {
            Console.WriteLine(SelectedConveyanceTxt);
            IsPrivateCar = true;
            IsCompanyCar = false;
            IsPublicTransport = false;
            formItem.PrivateVehicle = true;
            formItem.CompanyVehicle = false;
        }
        else if (SelectedConveyanceTxt == "Samochód służbowy")
        {
            Console.WriteLine(SelectedConveyanceTxt);
            IsPrivateCar = false;
            IsCompanyCar = true;
            IsPublicTransport = false;
            formItem.PrivateVehicle = false;
            formItem.CompanyVehicle = true;
            formItem.PublicTransport = false;
            formItem.PublicTransportPaid = false;
        }
        else if (SelectedConveyanceTxt == "Transport publiczny")
        {
            Console.WriteLine(SelectedConveyanceTxt);
            IsPrivateCar = false;
            IsCompanyCar = false;
            IsPublicTransport = true;
            formItem.PrivateVehicle = false;
            formItem.CompanyVehicle = false;
            formItem.PublicTransport = true;
            formItem.PublicTransportPaid = false;
        }

        if (formItem.Status == "Rozliczenie" || formItem.Status == "Ksiegowosc")
        {
            ShowTransits = true;
        }

        StateHasChanged();
    }
    private async Task HandlePublicTransportPaidChanged(bool value)
    {
        formItem.PublicTransportPaid = value;
    }
    private async Task HandleAdvancePaymentPaidChanged(bool value)
    {
        formItem.AdvancePayment = value;
        StateHasChanged();
    }
    private async Task HandleDateTimeChanged(DateTime? dateTime, string p)
    {
        if (dateTime.HasValue)
        {
            // Set seconds to 0
            dateTime = new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, dateTime.Value.Hour, dateTime.Value.Minute, 0);
        }
        if (p == "to")
        {
            formItem.EndDate = dateTime;
            if (formItem.Stages.Any())
            {
                formItem.Stages.Last().EndDate = dateTime;
            }
        }
        else if (p == "from")
        {
            formItem.StartDate = dateTime;
            if (formItem.Stages.Any())
            {
                formItem.Stages.First().StartDate = dateTime;
            }
        }

        RecalculateStages(0, "skip");
        RecalculateAccomodations();
        RecalculateMeals();
        RecalculateLocalTravels();
        ValidateDates();
    }
    private async Task HandleStageDateTimeChanged(DateTime? value, Stage stage, string propertyName, int stageId)
    {
        ConsoleLog("stage: " + stageId + " " + propertyName);
        ConsoleLog("stage: " + stageId + " start" + stage.StartDate.ToString() + " koniec: " + stage.EndDate.ToString());
        if (propertyName == nameof(stage.StartDate))
        {
            stage.StartDate = value;

            if (formItem.Stages.FirstOrDefault() == stage && propertyName == nameof(stage.StartDate))
            {
                // Update formItem.EndDate to the EndDate of the last stage
                formItem.StartDate = stage.StartDate;
            }
        }
        else if (propertyName == nameof(stage.EndDate))
        {
            stage.EndDate = value;
            if (formItem.Stages.LastOrDefault() == stage && propertyName == nameof(stage.EndDate))
            {
                // Update formItem.EndDate to the EndDate of the last stage
                formItem.EndDate = stage.EndDate;
            }

        }

        await RecalculateStages(stageId, propertyName);
        await RecalculateAccomodations();
        await RecalculateMeals();
        await RecalculateLocalTravels();
        await ValidateDates();
        StateHasChanged();
    }
    private async Task RecalculateStages(int? stageId, string? propertyName)
    {

        if (!(stageId == 0 && propertyName == "skip"))
        {
            var updatedStageIndex = formItem.Stages.FindIndex(s => s.Id == stageId);
            if (updatedStageIndex == -1)
                return;
            var updatedStage = formItem.Stages[updatedStageIndex];
            if (propertyName == nameof(updatedStage.EndDate) && updatedStage.EndDate.HasValue)
            {
                // Update the next stage's StartDate if it exists
                if (updatedStageIndex + 1 < formItem.Stages.Count)
                {
                    var nextStage = formItem.Stages[updatedStageIndex + 1];

                    // Set the next stage's StartDate to be one minute after the current EndDate
                    nextStage.StartDate = updatedStage.EndDate.Value.AddMinutes(1);
                }
            }

            if (propertyName == nameof(updatedStage.StartDate) && updatedStage.StartDate.HasValue)
            {
                // Update the previous stage's EndDate if it exists
                if (updatedStageIndex > 0)
                {
                    var previousStage = formItem.Stages[updatedStageIndex - 1];

                    // Set the previous stage's EndDate to be one minute before the current StartDate
                    previousStage.EndDate = updatedStage.StartDate.Value.AddMinutes(-1);
                }
            }
        }

        // Recalculate the duration for all stages
        formItem.SumAllowancePL = 0;
        formItem.SumAllowanceNotPL = 0;
        foreach (var stage in formItem.Stages)
        {
            if (stage.StartDate < stage.EndDate)
            {
                // Calculate duration and timespan
                stage.Duration = Math.Round((decimal)(stage.EndDate.Value - stage.StartDate.Value).TotalDays, 2);
                stage.timeSpan = TimeSpan.FromHours(Math.Round((stage.EndDate.Value - stage.StartDate.Value).TotalHours));

                // Reset Allowance values
                stage.AllowanceOriginValue = 0;
                stage.AllowanceAbroadValue = 0;
                stage.AllowanceOrigin = 0;
                stage.AllowanceAbroad = 0;

                if (stage.CountryCode == "PL")
                {
                    formItem.AllowancePL = stage.CountryAllowance;
                    // Calculate AllowanceOrigin factor and value
                    if (stage.timeSpan.TotalHours < 8)
                    {
                        stage.AllowanceOrigin = 0;
                    }
                    else if (stage.timeSpan.TotalHours >= 8 && stage.timeSpan.TotalHours < 12)
                    {
                        stage.AllowanceOrigin = 0.5m;
                        stage.AllowanceOriginValue = stage.CountryAllowance / 2;
                    }
                    else if (stage.timeSpan.TotalHours >= 12 && stage.timeSpan.TotalHours <= 24)
                    {
                        stage.AllowanceOrigin = 1;
                        stage.AllowanceOriginValue = stage.CountryAllowance;
                    }

                    if (stage.timeSpan.TotalHours > 24)
                    {
                        // Calculate for additional full days
                        var fullDays = (int)(stage.timeSpan.TotalHours / 24);
                        stage.AllowanceOrigin += fullDays;
                        stage.AllowanceOriginValue += fullDays * stage.CountryAllowance;

                        // Calculate for the remaining hours
                        var remainingHours = stage.timeSpan.TotalHours % 24;
                        if (remainingHours < 8)
                        {
                            stage.AllowanceOrigin += 0.5m;
                            stage.AllowanceOriginValue += stage.CountryAllowance / 2;
                        }
                        else
                        {
                            stage.AllowanceOrigin += 1;
                            stage.AllowanceOriginValue += stage.CountryAllowance;
                        }
                    }

                    formItem.SumAllowancePL += stage.AllowanceOriginValue;
                }
                else
                {
                    formItem.AllowanceNotPL = stage.CountryAllowance;
                    // Calculate AllowanceAbroad factor and value
                    if (stage.timeSpan.TotalHours < 8)
                    {
                        stage.AllowanceAbroad = 0.33m;
                        stage.AllowanceAbroadValue = Math.Round(stage.CountryAllowance / 3, 2);
                    }
                    else if (stage.timeSpan.TotalHours >= 8 && stage.timeSpan.TotalHours <= 12)
                    {
                        stage.AllowanceAbroad = 0.5m;
                        stage.AllowanceAbroadValue = stage.CountryAllowance / 2;
                    }
                    else if (stage.timeSpan.TotalHours >= 12 && stage.timeSpan.TotalHours <= 24)
                    {
                        stage.AllowanceAbroad = 1;
                        stage.AllowanceAbroadValue = stage.CountryAllowance;
                    }

                    if (stage.timeSpan.TotalHours > 24)
                    {
                        // Calculate for additional full days
                        var fullDays = (int)(stage.timeSpan.TotalHours / 24);
                        stage.AllowanceAbroad += fullDays;
                        stage.AllowanceAbroadValue += fullDays * stage.CountryAllowance;

                        // Calculate for the remaining hours
                        var remainingHours = stage.timeSpan.TotalHours % 24;
                        if (remainingHours > 0 && remainingHours < 8)
                        {
                            stage.AllowanceAbroad += 0.33m;
                            stage.AllowanceAbroadValue += Math.Round(stage.CountryAllowance / 3, 2);
                        }
                        else if (remainingHours >= 8 && remainingHours < 12)
                        {
                            stage.AllowanceAbroad += 0.5m;
                            stage.AllowanceAbroadValue += stage.CountryAllowance / 2;
                        }
                        else if (remainingHours >= 12 && remainingHours <= 24)
                        {
                            stage.AllowanceAbroad += 1;
                            stage.AllowanceAbroadValue += stage.CountryAllowance;
                        }

                    }

                    formItem.SumAllowanceNotPL += stage.AllowanceAbroadValue;
                }
            }
            else if (stage.StartDate >= stage.EndDate)
            {
                stage.timeSpan = new TimeSpan();
                stage.Duration = 0;
                stage.AllowanceOrigin = 0;
                stage.AllowanceOriginValue = 0;
                stage.AllowanceAbroad = 0;
                stage.AllowanceAbroadValue = 0;

            }
        }

    }
    private async Task RecalculateMeals()
    {
        formItem.DeductionMealsPL = 0;
        formItem.DeductionMealsNotPL = 0;
        foreach (var meal in formItem.Meals)
        {
            meal.Duration = 0;
            var stage = formItem.Stages[meal.StageId - 1];
            if (stage != null)
            {
                meal.Duration = (int)Math.Ceiling(stage.AllowanceOrigin.GetValueOrDefault() + stage.AllowanceAbroad.GetValueOrDefault());
                meal.Included = (stage.AllowanceOriginValue + stage.AllowanceAbroadValue) == 0 ? false : stage.Included;
                // meal.Total = (stage.AllowanceOrigin.GetValueOrDefault() + stage.AllowanceAbroad.GetValueOrDefault()) * meal.AllowanceRate;
                meal.Total = 0;
                if (meal.CountryCode != "PL")
                {
                    meal.Total -= (meal.CoveredBreakfasts * meal.AllowanceRate * 0.15m);
                    meal.Total -= (meal.CoveredLunches * meal.AllowanceRate * 0.3m);
                    meal.Total -= (meal.CoveredDinners * meal.AllowanceRate * 0.3m);
                    formItem.DeductionMealsNotPL += meal.Total;
                }
                else
                {
                    meal.Total -= (meal.CoveredBreakfasts * meal.AllowanceRate / 4);
                    meal.Total -= (meal.CoveredLunches * meal.AllowanceRate / 2);
                    meal.Total -= (meal.CoveredDinners * meal.AllowanceRate / 4);
                    formItem.DeductionMealsPL += meal.Total;
                }

                meal.Nights = CalculateNights(stage.StartDate.Value, stage.EndDate.Value);
                // var num = meal.Total;
                meal.Total = Math.Round(meal.Total, 2);
            }

            Console.WriteLine();
        }

        if (Math.Abs(formItem.DeductionMealsPL.GetValueOrDefault()) > Math.Abs(formItem.SumAllowancePL.GetValueOrDefault()))
        {
            formItem.DeductionMealsPL = -1 * formItem.SumAllowancePL;
        }
        if (Math.Abs(formItem.DeductionMealsNotPL.GetValueOrDefault()) > Math.Abs(formItem.SumAllowanceNotPL.GetValueOrDefault()))
        {
            formItem.DeductionMealsNotPL = -1 * formItem.SumAllowanceNotPL;
        }

    }
    private async Task RecalculateAccomodations()
    {
        formItem.AccomodationAllowanceSumPL = 0;
        formItem.AccomodationAllowanceSumNotPL = 0;
        foreach (var accommodation in formItem.Accommodations)
        {

            var stage = formItem.Stages[accommodation.StageId - 1];
            if (stage != null)
            {
                accommodation.Duration = stage.Duration;
                accommodation.Included = stage.Included;
                if (stage.StartDate.HasValue && stage.EndDate.HasValue)
                {
                    // Calculate the number of nights
                    accommodation.Nights = CalculateNights(stage.StartDate.Value, stage.EndDate.Value);
                }

                if (accommodation.Included)
                {
                    if (!accommodation.HasInvoices)
                    {
                        if (accommodation.CountryCode == "PL")
                        {
                            accommodation.Total = accommodation.Nights.GetValueOrDefault() * accommodation.AllowanceRate;
                            formItem.AccomodationAllowanceSumPL += accommodation.Total;
                        }
                        else
                        {
                            accommodation.Total = accommodation.Nights.GetValueOrDefault() * accommodation.AllowanceRate / 4;
                            formItem.AccomodationAllowanceSumNotPL += accommodation.Total;
                        }

                    }
                }
                else
                {
                    accommodation.Total = 0;
                }

            }
        }
    }
    private async Task RecalculateLocalTravels()
    {
        formItem.SumLocalTravelAllowancePL = 0;
        formItem.SumLocalTravelAllowanceNotPL = 0;


        foreach (var localTravel in formItem.LocalTravels)
        {
            var stage = formItem.Stages[localTravel.StageId - 1];
            localTravel.Duration = (int)Math.Ceiling(stage.AllowanceOrigin.GetValueOrDefault() + stage.AllowanceAbroad.GetValueOrDefault());
            localTravel.Total = localTravel.Days * localTravel.AllowanceRate;

        }
        formItem.SumLocalTravelAllowancePL = formItem.LocalTravels.Where(lt => lt.CountryCode == "PL").Sum(lt => lt.Total);
        formItem.SumLocalTravelAllowanceNotPL = formItem.LocalTravels.Where(lt => lt.CountryCode != "PL").Sum(lt => lt.Total);
    }
    private async Task RecalculateTransit()
    {

        if (formItem.Transit != null)
        {
            formItem.Transit.Total = formItem.Transit.Directions * formItem.Transit.AllowanceRate;
        }
    }
    private int CalculateNights(DateTime startDate, DateTime endDate)
    {
        int nights = 0;

        // Adjust the time range to the period we care about (9:00 PM to 7:00 AM)
        TimeSpan nightStart = new TimeSpan(21, 0, 0); // 9:00 PM
        TimeSpan nightEnd = new TimeSpan(7, 0, 0); // 7:00 AM

        DateTime currentStart = startDate;
        DateTime currentEnd = endDate;

        // Loop through each day in the range
        while (currentStart.Date <= currentEnd.Date)
        {
            DateTime nightStartTime = currentStart.Date.Add(nightStart);
            DateTime nightEndTime = currentStart.Date.AddDays(1).Add(nightEnd);

            // Determine overlap between the stage time and the night period
            DateTime overlapStart = nightStartTime > currentStart ? nightStartTime : currentStart;
            DateTime overlapEnd = nightEndTime < currentEnd ? nightEndTime : currentEnd;

            if (overlapStart < overlapEnd)
            {
                // Calculate the duration of overlap
                TimeSpan overlapDuration = overlapEnd - overlapStart;

                // Count continuous 6-hour periods
                nights += (int)(overlapDuration.TotalHours / 6);
            }

            // Move to the next day
            currentStart = currentStart.AddDays(1).Date;
        }

        return nights;
    }
    private async Task ValidateDates()
    {
        // Check if the overall start date is before the end date
        if (formItem.StartDate > formItem.EndDate)
        {
            DateTimeValidationMessage = "Start date must be before or equal to end date.";
        }
        else
        {
            DateTimeValidationMessage = string.Empty;
        }
        var invalidStage = formItem.Stages
            .Select((stage, index) => new { stage, index })
            .FirstOrDefault(x =>
                x.stage.StartDate > x.stage.EndDate ||
                (x.index < formItem.Stages.Count - 1 && x.stage.EndDate > formItem.Stages[x.index + 1].StartDate) ||
                (x.index > 0 && x.stage.StartDate < formItem.Stages[x.index - 1].EndDate));

        var errorMessages = new List<string>();

        for (int i = 0; i < formItem.Stages.Count; i++)
        {
            var stage = formItem.Stages[i];

            if (stage.StartDate > stage.EndDate)
            {
                errorMessages.Add($"Etap {stage.Id}: Data początkowa musi być wcześniejsza niż końcowa");
            }

            if (i < formItem.Stages.Count - 1 && stage.EndDate > formItem.Stages[i + 1].StartDate)
            {
                errorMessages.Add($"Data końcowa Etapu {stage.Id} musi być wcześniejsza niż data początkowa Etapu {formItem.Stages[i + 1].Id}.");
            }

            if (i > 0 && stage.StartDate < formItem.Stages[i - 1].EndDate)
            {
                errorMessages.Add($"Data początkowa Etapu {stage.Id} musi być późniejsza niż data końcowa Etapu {formItem.Stages[i - 1].Id}.");
            }
        }

        if (errorMessages.Any())
        {
            StagesDatesErrorMessages = errorMessages;
            StagesDatesErrorMessage = string.Join("<br>", errorMessages);
        }
        else
        {
            // Clear the error message if all dates are valid
            StagesDatesErrorMessages = new();
            StagesDatesErrorMessage = string.Empty;
        }
        // Check intermediate dates within stages using LINQ
        // var invalidStage = formItem.Stages
        //     .Select((stage, index) => new { stage, index })
        //     .FirstOrDefault(x =>
        //         x.stage.StartDate > x.stage.EndDate ||
        //         (x.index < formItem.Stages.Count - 1 && x.stage.EndDate >= formItem.Stages[x.index + 1].StartDate));
        //
        // if (invalidStage != null)
        // {
        //     if (invalidStage.stage.StartDate > invalidStage.stage.EndDate)
        //     {
        //         StagesDatesErrorMessage = $"Stage {invalidStage.stage.Id}: Start date must be before or equal to end date.";
        //     }
        //     else if (invalidStage.stage.EndDate > invalidStage.stage.StartDate)
        //     {
        //         StagesDatesErrorMessage = $"Stage {invalidStage.stage.Id}: End date must be before the start date of the next stage.";
        //     }
        // }
        // else
        // {
        //     // Clear the error message if all dates are valid
        //     StagesDatesErrorMessage = string.Empty;
        // }
        Console.WriteLine();
    }

    #endregion OnChange
    #region Actions

    private async Task AddEmployee(EmployeeVm newEmployee)
    {
        var employeesList = new List<EmployeeVm>();

        employeesList.Add(newEmployee);

        SelectedEmpl = employeesList;
    }
    private async Task RemoveBill(Bill bill)
    {
        RemoveFileFromBill(bill);
        formItem.Bills.Remove(bill);
    }
    private async Task AddBill()
    {
        formItem.Bills.Add(new Bill()
        {
            Id = formItem.Bills.Count() + 1,
            InvoiceMappings = new List<InvoiceMapping>
                {
                    new InvoiceMapping()
                }

        });
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

    #endregion
    #region FilesManagement

    private async Task HandleFileUpload(InputFileChangeEventArgs e)
    {
        if (CurrentBillForUpload != null)
        {
            var uploadedFile = e.File;
            if (uploadedFile != null)
            {
                CurrentBillForUpload.FilePath = await FileUtils.UploadFile(uploadedFile, CurrentBillForUpload.Id.ToString(), Environment.WebRootPath);
                CurrentBillForUpload.OriginalFileName = uploadedFile.Name;
                CurrentBillForUpload.AttUrl = FileUtils.GenerateUrl(CurrentBillForUpload.FilePath, Environment.WebRootPath);
            }
        }
    }
    private void RemoveFileFromBill(Bill bill)
    {
        FileUtils.RemoveFile(bill.FilePath);
        bill.FilePath = null;
        bill.OriginalFileName = null;
        bill.AttUrl = null;
    }
    private void UploadFileForBill(Bill bill)
    {
        CurrentBillForUpload = bill;
        JS.InvokeVoidAsync("triggerFileUpload");
    }
    private void SaveBillFiles()
    {
        foreach (var bill in formItem.Bills)
        {
            if (!string.IsNullOrEmpty(bill.FilePath))
            {
                var newFilePath = Path.Combine(
                    Environment.WebRootPath,
                    "BusinessTravels",
                    formItem.CreatedDate.ToString("yyyy"),
                    formItem.CreatedDate.ToString("yyyyMM"),
                    formItem.CreatedDate.ToString("yyyyMMdd"),
                    $"{Guid.NewGuid()}_{bill.Id}{Path.GetExtension(bill.FilePath)}");

                FileUtils.MoveFile(bill.FilePath, newFilePath);
                bill.FilePath = newFilePath;

                // Update the public-facing URL for the file.
                bill.AttUrl = FileUtils.GenerateUrl(newFilePath, Environment.WebRootPath);
            }
        }
    }

    #endregion
    #region SetupForm

    private void ResetForm()
    {
        ShowAccomodations = false;
        ShowMeals = false;
        ShowStages = false;
        ShowLocalTravels = false;
        ShowTransits = false;
        ShowCashier = false;
        ShowBills = false;
        ShowSummary = false;
        EnterInvoiceMapping = false;
        StateHasChanged();
    }
    private async Task SetupForm(string status)
    {
        _organisation = await _mediator.Send(new GetOrganisationByEmpSapNumberQuery(_userContext.Employee.SapNumber));
        ResetForm();

        await ConsoleLog($"Selected status: {formItem.Status} \n ShowAccomodations = {ShowAccomodations.ToString()} \n ShowMeals = {ShowMeals.ToString()} \n  ShowStages = {ShowStages.ToString()} \n  ShowLocalTravels = {ShowLocalTravels.ToString()} \n  ShowTransits = {ShowTransits.ToString()} \n  ShowCashier = {ShowCashier.ToString()} \n  ShowBills = {ShowBills.ToString()} \n  ShowSummary = {ShowSummary.ToString()} \n  EnterInvoiceMapping = {EnterInvoiceMapping.ToString()}");

        if (status == "Rejestracja")
            SetupFormRejestracja();
        else if (status.StartsWith("AprobataL"))
            SetupFormAprobata(status);
        else if (status == "Kasa")
            SetupFormKasa();
        else if (status == "Rozliczenie")
            SetupFormRozliczenie();
        else if (status == "Ksiegowosc")
            SetupFormKsiegowosc();
        else if (status == "Rozliczone")
            SetupFormRozliczone();
        else if (status == "KasaRozliczenie")
            SetupFormKasaRozliczenie();
        else if (status == "Zamkniete")
            SetupFormZamkniete();
        StateHasChanged();
        await ConsoleLog($"Selected status: {formItem.Status} \n ShowAccomodations = {ShowAccomodations.ToString()} \n ShowMeals = {ShowMeals.ToString()} \n  ShowStages = {ShowStages.ToString()} \n  ShowLocalTravels = {ShowLocalTravels.ToString()} \n  ShowTransits = {ShowTransits.ToString()} \n  ShowCashier = {ShowCashier.ToString()} \n  ShowBills = {ShowBills.ToString()} \n  ShowSummary = {ShowSummary.ToString()} \n  EnterInvoiceMapping = {EnterInvoiceMapping.ToString()}");

    }
    private async Task SetupFormRejestracja()
    {
        Title = "Podróż służbowa - Rejestracja";
        await AddEmployee(_userContext.Employee);
        formItem.EmployeeName = _userContext.LongName;
        formItem.EnovaEmpId = _userContext.EnovaEmpId;
        _organisation = await _mediator.Send(new GetOrganisationByEmpSapNumberQuery(_userContext.Employee.SapNumber));
        formItem.OrganisationSapNumber = _organisation.SapNumber;

        ShowAccomodations = false;
        ShowMeals = false;
        ShowStages = false;
        ShowLocalTravels = false;
        ShowTransits = false;
        ShowCashier = false;
        ShowBills = false;
        ShowSummary = false;
        EnterInvoiceMapping = false;

        IsAdvancedPaymentDisabled = false;
        IsDateTimeDisabled = false;
        IsDestinationDisabled = false; //wyłącza możliwość zmiany celu podróży
        IsEmployeeDisabled = false; //wyłącza możliwość zmiany pracownika
        IsInvoiceMappingDisabled = true;

        IsTransportationDisabled = false;
        IsMileageDisabled = false;
        IsCashPayoutNumberDisabled = true;

        StateHasChanged();
    }
    private async Task SetupFormKasa()
    {
        Title = "Podróż służbowa - Kasa";
        ShowAccomodations = false;
        ShowMeals = false;
        ShowStages = false;
        ShowLocalTravels = false;
        ShowTransits = false;
        ShowCashier = false;
        ShowBills = false;
        ShowSummary = false;
        EnterInvoiceMapping = false;

        IsAdvancedPaymentDisabled = true;
        IsDateTimeDisabled = true;
        IsDestinationDisabled = true;
        IsEmployeeDisabled = true;
        IsInvoiceMappingDisabled = true;

        IsTransportationDisabled = true;
        IsMileageDisabled = true;
        IsCashPayoutNumberDisabled = false;

        formItem.PayoutCashier = _userContext.Employee;
        formItem.PayoutCashierEmpId = _userContext.EnovaEmpId;
    }
    private async Task SetupFormRozliczenie()
    {
        Title = "Podróż służbowa - Rozliczenie";
        ShowAccomodations = true;
        ShowMeals = true;
        ShowStages = true;
        ShowLocalTravels = true;
        ShowTransits = formItem.Stages.Count > 1;
        ShowCashier = true;
        ShowBills = true;
        ShowSummary = true;
        EnterInvoiceMapping = true;

        IsAdvancedPaymentDisabled = false; //w produkcji zmienić na true
        IsDateTimeDisabled = false;
        IsDestinationDisabled = false; //w produkcji zmienić na true
        IsEmployeeDisabled = true;

        IsBillsDisabled = false;
        IsInvoiceMappingDisabled = false;
        IsTransportationDisabled = false;
        IsTransportationDisabled = true;
        IsMileageDisabled = false;
        IsCashPayoutNumberDisabled = true;



    }
    private async Task SetupFormKsiegowosc()
    {
        Title = "Podróż służbowa - Księgowość";
        ShowAccomodations = true;
        ShowMeals = true;
        ShowStages = true;
        ShowLocalTravels = true;
        ShowTransits = formItem.Stages.Count > 1;
        ShowCashier = true;
        ShowBills = true;
        ShowSummary = true;
        EnterInvoiceMapping = true;

        IsAdvancedPaymentDisabled = true;
        IsDateTimeDisabled = false;
        IsDestinationDisabled = true;
        IsEmployeeDisabled = true;
        IsBillsDisabled = true;
        IsInvoiceMappingDisabled = false;

        IsTransportationDisabled = true;
        IsMileageDisabled = true;
        IsCashPayoutNumberDisabled = true;

    }
    private async Task SetupFormKasaRozliczenie()
    {
        Title = "Podróż służbowa - Rozliczenie Kasa";
        ShowAccomodations = false;
        ShowMeals = false;
        ShowStages = false;
        ShowLocalTravels = false;
        ShowTransits = false;
        ShowCashier = false;
        ShowBills = false;
        ShowSummary = false;
        EnterInvoiceMapping = false;

        IsAdvancedPaymentDisabled = true;
        IsDateTimeDisabled = true;
        IsDestinationDisabled = true;
        IsEmployeeDisabled = true;
        IsInvoiceMappingDisabled = true;

        IsTransportationDisabled = true;
        IsMileageDisabled = true;
        IsCashPayoutNumberDisabled = true;

        formItem.ReceiptCashier = _userContext.Employee;
        formItem.ReceiptCashierEmpId = _userContext.EnovaEmpId;

    }
    private async Task SetupFormRozliczone()
    {
        Title = "Podróż służbowa - Rozliczone";
        ShowAccomodations = false;
        ShowMeals = false;
        ShowStages = false;
        ShowLocalTravels = false;
        ShowTransits = false;
        ShowCashier = false;
        ShowBills = false;
        ShowSummary = false;
        EnterInvoiceMapping = false;

        IsAdvancedPaymentDisabled = true;
        IsDateTimeDisabled = true;
        IsDestinationDisabled = true;
        IsEmployeeDisabled = true;
        IsBillsDisabled = true;
        IsInvoiceMappingDisabled = true;

        IsTransportationDisabled = true;
        IsMileageDisabled = true;
        IsCashPayoutNumberDisabled = true;

    }
    private async Task SetupFormZamkniete()
    {
        Title = "Podróż służbowa - Zamknięte";
        ShowAccomodations = false;
        ShowMeals = false;
        ShowStages = false;
        ShowLocalTravels = false;
        ShowTransits = false;
        ShowCashier = false;
        ShowBills = false;
        ShowSummary = false;
        EnterInvoiceMapping = false;

        IsAdvancedPaymentDisabled = true;
        IsDateTimeDisabled = true;
        IsDestinationDisabled = true;
        IsEmployeeDisabled = true;
        IsBillsDisabled = true;

        IsTransportationDisabled = true;
        IsMileageDisabled = true;
        IsCashPayoutNumberDisabled = true;
    }
    private async Task SetupFormAprobata(string status)
    {
        Title = $"Podróż służbowa - {status}";
        if (status == "AprobataL1")
        {
            ShowAccomodations = false;
            ShowMeals = false;
            ShowStages = false;
            ShowLocalTravels = false;
            ShowTransits = false;
            ShowCashier = false;
            ShowBills = false;
            ShowSummary = false;

        }
        else if (status == "AprobataL2")
        {
            ShowAccomodations = false;
            ShowMeals = false;
            ShowStages = false;
            ShowLocalTravels = false;
            ShowTransits = false;
            ShowCashier = false;
            ShowBills = false;
            ShowSummary = false;

        }
        else if (status == "AprobataL11")
        {
            ShowAccomodations = false;
            ShowMeals = false;
            ShowStages = false;
            ShowLocalTravels = false;
            ShowTransits = false;
            ShowCashier = false;
            ShowBills = true;
            ShowSummary = true;

        }
        else if (status == "AprobataL12")
        {
            ShowAccomodations = false;
            ShowMeals = false;
            ShowStages = false;
            ShowLocalTravels = false;
            ShowTransits = false;
            ShowCashier = false;
            ShowBills = true;
            ShowSummary = true;


        }
        EnterInvoiceMapping = false;
        IsAdvancedPaymentDisabled = true;
        IsDateTimeDisabled = true;
        IsDestinationDisabled = true;
        IsEmployeeDisabled = true;
        IsBillsDisabled = true;
        IsInvoiceMappingDisabled = true;

        IsTransportationDisabled = true;
        IsMileageDisabled = true;
        IsCashPayoutNumberDisabled = true;
    }

    private bool IsCashPayoutEnabled() //pole do wpisania numeru KW
    {
        return formItem.Status == "Kasa";
    }
    private bool IsCashReceiptEnabled() //pole do wpisania numeru KW
    {
        return formItem.Status == "KasaRozliczenie";
    }
    #endregion
    private void HandleValidSubmit(EditContext arg)
    {
        throw new NotImplementedException();
    }

}