using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models.ExternalConnectors;

namespace Persistance;
public class AsDbContextFactory : DesignTimeDbContextFactoryBase<AsDbContext>
{
    private readonly IConfiguration _configuration;
    private const string ConnectionStringName = "AsDbContextConnection";

    public AsDbContextFactory(IConfiguration configuration) : base(ConnectionStringName)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    protected override AsDbContext CreateNewInstance(DbContextOptions<AsDbContext> options)
    {
        return new AsDbContext(options, _configuration);
    }

}