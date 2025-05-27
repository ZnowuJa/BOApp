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
public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Qty).HasColumnType("decimal(10,2)");
        builder.Property(p => p.UnitNetPrice).HasColumnType("decimal(10,2)");
    }
}
