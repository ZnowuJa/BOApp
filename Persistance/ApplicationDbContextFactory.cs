using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance;
public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
    protected override AppDbContext CreateNewInstance(
        DbContextOptions<AppDbContext> options)
    {
        return new AppDbContext(options);
    }

}
