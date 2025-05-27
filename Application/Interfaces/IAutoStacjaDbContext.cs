using Domain.Entities.ITTools.LicenceAutoStacja;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces

{
    public interface IAutoStacjaDbContext
    {
        DbSet<MysystemPunkt> MysystemPunkts { get; set; }
        DbSet<MysystemPunktCon> MysystemPunktCons { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    
}
