using Domain.Entities.Accounting;
using Domain.Entities.Administration;
using Domain.Entities.BusinessOperations;
using Domain.Entities.CoC;
using Domain.Entities.Common;
using Domain.Entities.ITTools.LicenceAutoStacja;
using Domain.Entities.ITWarehouse;
using Domain.Forms;
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
    DbSet<CostCenter> CostCenters { get; set; }
    DbSet<Country> Countries { get; set; }
    DbSet<GLAccount> GLAccounts { get; set; }
    DbSet<VATRate> VATRates { get; set; }
    DbSet<AccountingNoteForm> AccountingNotes { get; set; }
    DbSet<CompanyCarRegistrationNumber> CompanyCarRegistrationNumbers { get; set; }
    DbSet<NbpCurrencyRate> NbpCurrencyRates { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
}