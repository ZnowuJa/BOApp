using Domain.Entities.Common;
using Domain.Entities.ITWarehouse;
using Domain.Forms;
using Domain.WorkFlows;

using Microsoft.EntityFrameworkCore;
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
    public DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
    public DbSet<WorkflowStep> WorkflowSteps { get; set; }
    public DbSet<Organisation> Organisations { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}