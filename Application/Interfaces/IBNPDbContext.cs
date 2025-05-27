using Application.ViewModels.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.BNP;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
namespace Application.Interfaces

{
    public interface IBNPDbContext
    {
        DbSet<Bnp20> Bnp20s { get; set; }
        DbSet<Bnp55> Bnp55s { get; set; }
    }
}
