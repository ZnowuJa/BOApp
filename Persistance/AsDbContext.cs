using Application.Interfaces;
using BackOfficeApp_Domain.Common;
using Domain.Entities.Accounting;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistance;
public class AsDbContext : DbContext, IAsDbContext
{
    private readonly IConfiguration _configuration;
    //private readonly string _environment;

    public AsDbContext(DbContextOptions<AsDbContext> options) : base(options)
    {
    }
    //public AsDbContext(DbContextOptions<AsDbContext> options, IConfiguration configuration) : base(options)
    //{
    //    _configuration = configuration;
    //    //_environment = _configuration.GetValue<string>("Environment");
    //}

    public DbSet<Customer> v_KONTRAHENCI_LISTA { get; set; }
    
    

    Task<int> IAsDbContext.SaveChangesAsync(CancellationToken cancellationToken)
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Console.WriteLine(_environment);
        //Console.WriteLine(_environment == "PROD" ? "v_KONTRAHENCI_LISTA" : "Kontrahenci");
        builder.Entity<Customer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("v_KONTRAHENCI_LISTA");

            entity.Property(e => e.KontrahentId).HasColumnName("KONTRAHENT_ID");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .UseCollation("Polish_CI_AS")
                .HasColumnName("NAZWA");
            entity.Property(e => e.Nip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Polish_CI_AS")
                .HasColumnName("NIP");
            entity.Property(e => e.Przelew).HasColumnName("PRZELEW");
        });

        base.OnModelCreating(builder);
    }
    public Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
    {
        return Database.ExecuteSqlRawAsync(sql, parameters);
    }
    public Task<int> ChangePaymentMethod(int paymentMethod, int customerId)
    {
        // Call the stored procedure on the remote server
        try
        {
            // Call the stored procedure on the remote server
            var sql = "EXEC dbo.ChangePaymentMethod @PaymentMethod, @CustomerID";
            Database.ExecuteSqlRaw(sql, new SqlParameter("@PaymentMethod", paymentMethod), new SqlParameter("@CustomerID", customerId));
            Console.WriteLine(sql);

            // Return success
            return Task.FromResult(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            // Return failure
            return Task.FromResult(0);
        }
    }

}