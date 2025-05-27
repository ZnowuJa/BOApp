using Domain.Entities.Accounting;
using Domain.Entities.Administration;
using Domain.Entities.BNP;
using Domain.Entities.BusinessOperations;
using Domain.Entities.CoC;
using Domain.Entities.Common;
using Domain.Entities.ITTools.LicenceAutoStacja;
using Domain.Entities.ITWarehouse;
using Domain.Forms;
using Domain.Forms.Accounting;
using Domain.Forms.ITForms;
using Domain.WorkFlows;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces;
public interface IAppDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<CategoryType> CategoryTypes { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<CompanyType> CompanyTypes { get; set; }
    DbSet<Currency> Currencies { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeType> EmployeeTypes { get; set; }
    DbSet<InvoiceItem> InvoiceItems { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    DbSet<Asset> Assets { get; set; }
    DbSet<Asset_Note> Assets_Notes { get; set; }
    DbSet<Note> Notes { get; set; }
    DbSet<Part> Parts { get; set; }
    DbSet<State> States { get; set; }
    DbSet<Unit> Units { get; set; }
    DbSet<Vendor> Vendors { get; set; }
    DbSet<Warehouse> Warehouses { get; set; }
    DbSet<EmpInfo> EmpInfos { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<AssetHistory> AssetsHistory { get; set; }
    DbSet<DeferralPaymentForm> DeferralPayments { get; set; }
    DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
    DbSet<WorkflowStep> WorkflowSteps { get; set; }
    DbSet<Organisation> Organisations { get; set; }
    DbSet<TestForm> TestForms { get; set; }
    DbSet<FormFile> FormFiles { get; set; }
    public DbSet<GroupCoC> Groups { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<InstructionCoC> Instructions { get; set; }
    public DbSet<BackgroundJob> BackgroundJobs { get; set; }
    public DbSet<OnboardingForm> OnboardingForms { get; set; }
    public DbSet<ITScrappingForm> ITScrappingForms { get; set; }
    public DbSet<ITSaleForm> ITSaleForms { get; set; }
    public DbSet<CostCenter> CostCenters { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<GLAccount> GLAccounts { get; set; }
    public DbSet<VATRate> VATRates { get; set; }
    public DbSet<AccountingNoteForm> AccountingNotes { get; set; }
    public DbSet<ManagerDeputy> ManagerDeputies { get; set; }
    public DbSet<CompanyCarRegistrationNumber> CompanyCarRegistrationNumbers { get; set; }
    public DbSet<NbpCurrencyRate> NbpCurrencyRates { get; set; }
    public DbSet<BusinessTravelForm> BusinessTravels { get; set; }
    public DbSet<AdvancePaymentForm> AdvancePayments { get; set; }
    public DbSet<Bnp20> Bnp20s { get; set; }
    public DbSet<Bnp55> Bnp55s { get; set; }
    public DbSet<SapCostCenter> SapCostCenters { get; set; }
    public DbSet<BusinessPartner> BusinessPartners { get; set; }
    public DbSet<BankTransferForm> BankTransfers{ get; set; }
    public DbSet<CostAllocation> CostAllocations { get; set; }
    public DbSet<ErrorLog> ErrorLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
}