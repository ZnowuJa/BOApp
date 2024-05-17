using Domain.Entities.ITWarehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations;
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>

{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.TotalNet).HasColumnType("decimal(10,2)");
    
    }
}
