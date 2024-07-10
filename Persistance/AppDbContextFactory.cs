using Microsoft.EntityFrameworkCore;

namespace Persistance;
public class AppDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
    private const string ConnectionStringName = "AppDbContextConnection";

    public AppDbContextFactory() : base(ConnectionStringName) 
    { 
    }

    protected override AppDbContext CreateNewInstance(
        DbContextOptions<AppDbContext> options)
    {
        return new AppDbContext(options);
    }

    
}
