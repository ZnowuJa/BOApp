using Microsoft.EntityFrameworkCore;
using Domain.Entities.AutoStacja;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Entities.AutoStacja;
using Persistance.AutoStacja;

namespace Application.Interfaces

{
    public interface IAutoStacjaDbContext
    {
        DbSet<MysystemPunkt> MysystemPunkts { get; set; }
        DbSet<MysystemPunktCon> MysystemPunktCons { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    
}
