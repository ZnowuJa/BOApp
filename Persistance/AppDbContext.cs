using Application.Entities;
using Application.Interfaces;
using BackOfficeApp_Domain.Common;
using Domain.Entities.Accounting;
using Domain.Entities.Administration;
using Domain.Entities.BNP;
using Domain.Entities.BusinessOperations;
using Domain.Entities.CoC;
using Domain.Entities.Common;
using Domain.Entities.ITWarehouse;
using Domain.Entities.ITTools.LicenceAutoStacja;
using Domain.Forms;
using Domain.Forms.Accounting;
using Domain.Forms.ITForms;
using Domain.WorkFlows;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Emit;

namespace Persistance;
public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CategoryType> CategoryTypes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CompanyType> CompanyTypes { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeType> EmployeeTypes { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Asset_Note> Assets_Notes { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<EmpInfo> EmpInfos { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<AssetHistory> AssetsHistory { get; set; }
    public DbSet<DeferralPaymentForm> DeferralPayments { get; set; }
    public DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
    public DbSet<WorkflowStep> WorkflowSteps { get; set; }
    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<TestForm> TestForms { get; set; }
    public DbSet<FormFile> FormFiles { get; set; }
    public DbSet<GroupCoC> Groups { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<InstructionCoC> Instructions { get; set; }
    public DbSet<BackgroundJob> BackgroundJobs { get; set; }
    public DbSet<OnboardingForm> OnboardingForms { get; set; }
    public DbSet<CostCenter> CostCenters { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<GLAccount> GLAccounts { get; set; }
    public DbSet<VATRate> VATRates { get; set; }
    public DbSet<ITScrappingForm> ITScrappingForms { get; set; }
    public DbSet<ITSaleForm> ITSaleForms { get; set; }
    public DbSet<AccountingNoteForm> AccountingNotes { get; set; }
    public DbSet<ManagerDeputy> ManagerDeputies { get; set; }
    public DbSet<CompanyCarRegistrationNumber> CompanyCarRegistrationNumbers { get ; set; }
    public DbSet<NbpCurrencyRate> NbpCurrencyRates {  get; set; }
    public DbSet<BusinessTravelForm> BusinessTravels { get; set; }
    public DbSet<SapCostCenter> SapCostCenters { get; set; }
    public DbSet<Bnp20> Bnp20s { get; set; }
    public DbSet<Bnp55> Bnp55s { get; set; }
    public DbSet<CostAllocation> CostAllocations { get; set; }
    public DbSet<AdvancePaymentForm> AdvancePayments { get; set; }
    public DbSet<BusinessPartner> BusinessPartners { get; set; }
    public DbSet<BankTransferForm> BankTransfers{ get; set; }

    public DbSet<ErrorLog> ErrorLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BusinessTravelForm>(e =>
        {
            e.Property(e => e.AdvancePaymentAmount).HasColumnType("decimal(10,2)");
            e.Property(e => e.AdvancePaymentCash).HasColumnType("bit");
            e.Property(e => e.CurrencyExchangeRate).HasColumnType("decimal(10,4)");
            e.Property(e => e.AllowancePL).HasColumnType("decimal(10,2)");
            e.Property(e => e.AllowanceNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.SumAllowancePL).HasColumnType("decimal(10,2)");
            e.Property(e => e.SumAllowanceNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.DeductionMealsPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.DeductionMealsNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.AccomodationAllowanceSumPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.AccomodationAllowanceSumNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.SumLocalTravelAllowancePL).HasColumnType("decimal(10,2)");
            e.Property(e => e.SumLocalTravelAllowanceNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.SumPrivateVehicleAllowance).HasColumnType("decimal(10,2)");
            e.Property(e => e.TotalBillsPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.TotalBillsNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.TotalAllowancePL).HasColumnType("decimal(10,2)");
            e.Property(e => e.TotalAllowanceNotPL).HasColumnType("decimal(10,2)");
            e.Property(e => e.TotalPayOut).HasColumnType("decimal(10,2)");
        });
        builder.Entity<Invoice>(e =>
        {
            e.Property(e => e.TotalNet).HasColumnType("decimal(10,2)");
        });
        builder.Entity<InvoiceItem>(e =>
        {
            e.Property(p => p.Qty).HasColumnType("decimal(10,2)");
            e.Property(p => p.UnitNetPrice).HasColumnType("decimal(10,2)");
        });
        builder.Entity<Asset>(e =>
        {
            e.Property(e => e.Price).HasColumnType("decimal(10,2)");
        });
        builder.Entity<Asset>(e =>
        {
            e.Property(e => e.Price).HasColumnType("decimal(10,2)");
        });
        builder.Entity<Organisation>().ToTable("Organisations");

        builder.Entity<GroupCoC>()
            .HasMany(g => g.Positions)
            .WithOne(p => p.GroupCoC)
            .HasForeignKey(p => p.GroupCoCId)
            .IsRequired(false);
        builder.Entity<InstructionCoC>()
            .HasMany(x => x.Groups)
            .WithMany(y => y.Instructions)
            .UsingEntity(e => e.ToTable("InstructionGroup"));
        builder.Entity<SalonInfo>()
            .ToView("v_SalonInfo")
            .HasNoKey(); // Mapa do widoku w bazie danych

        builder.Entity<CompanyCarRegistrationNumber>()
            .ToView("v_CompanyCars")
            .HasKey(c => c.RegistrationNumber);
        builder.Entity<CompanyCarRegistrationNumber>()
            .Property(c => c.RegistrationNumber)
            .HasColumnName("registration_number");
        builder.Entity<NbpCurrencyRate>()
            .ToView("v_NBPExchangeRates")
            .HasKey(c => c.Id);
        builder.Entity<NbpCurrencyRate>()
            .Property(c => c.RateDate)
            .HasColumnName("Rate_date");
        builder.Entity<Bnp20>()
           .ToView("v_BNP20")
           .HasNoKey();
        builder.Entity<Bnp55>()
           .ToView("v_BNP55")
           .HasNoKey();

        builder.Entity<ManagerDeputy>()
        .HasIndex(m => m.ManagerId)
        .IsUnique();

        builder.Entity<CostAllocation>()
            .ToView("v_EmpCostAllocations")
            .HasNoKey();
        
        
        base.OnModelCreating(builder);

    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = string.Empty;
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.StatusId = 1;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = string.Empty;
                    entry.Entity.Modified = DateTime.Now;
                    break;
                case EntityState.Deleted:
                    entry.Entity.ModifiedBy = string.Empty;
                    entry.Entity.Modified = DateTime.Now;
                    entry.Entity.Inactivated = DateTime.Now;
                    entry.Entity.InactivatedBy = string.Empty;
                    entry.Entity.StatusId = 0;
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SmallAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.StatusId = 1;
                    break;

                case EntityState.Deleted:
                    entry.Entity.Inactivated = DateTime.Now;
                    entry.Entity.InactivatedBy = string.Empty;
                    entry.Entity.StatusId = 0;
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await this.Database.BeginTransactionAsync(cancellationToken);
    }

    public override EntityEntry Entry(object entity)
    {
        return base.Entry(entity);
    }
}
