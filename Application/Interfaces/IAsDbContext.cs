using Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;
public interface IAsDbContext
{
    public DbSet<Customer> v_KONTRAHENCI_LISTA { get; set; }
    // Define other DbSet properties for your entities
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);
    Task<int> ChangePaymentMethod(int paymentMethod, int customerId);
}
