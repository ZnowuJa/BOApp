using Domain.Entities.ITWarehouse;
using Microsoft.EntityFrameworkCore;

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


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}