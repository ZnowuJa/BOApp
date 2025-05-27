using Domain.Entities.ITTools.LicenceAutoStacja;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDiscounts2AS
    {
        DbSet<SalonInfo> SalonInfos { get; set; }
    }
}
