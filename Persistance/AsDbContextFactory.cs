using Microsoft.EntityFrameworkCore;

namespace Persistance;
public class AsDbContextFactory : DesignTimeDbContextFactoryBase<AsDbContext>
{
    //private readonly IConfiguration _configuration;
    private const string ConnectionStringName = "AsDbContextConnection";

    public AsDbContextFactory() : base(ConnectionStringName)
    {
        
    }

    protected override AsDbContext CreateNewInstance(DbContextOptions<AsDbContext> options)
    {
        return new AsDbContext(options);
    }

}