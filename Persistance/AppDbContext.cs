using Application.Entities;
using Application.Interfaces;
using BackOfficeApp_Domain.Common;
using Domain.Entities.ITWarehouse;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Persistance;
public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
{
    //private readonly IDateTime _dateTime;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }


    //public AppDbContext(DbContextOptions<AppDbContext> options, IDateTime dateTime) : base(options)
    //{
    //    _dateTime = dateTime;
    //}
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


    protected override void OnModelCreating(ModelBuilder builder)
    {
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

        base.OnModelCreating(builder);

    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
    //    //modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
    //    //modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
    //    //here we should inform about ValueObjects
    //    // modelBuilder.Entity<Director>().OwnsOne(p=>p.DirectorName;
    //    // w powyższym DirectorName w Director jest
    //    //  ValueObjectem PersonName


    //    //konfiguracja wymagań przeniesiona do Configurations
    //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    //}
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
}
