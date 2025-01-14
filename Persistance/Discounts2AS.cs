using System;
using System.Collections.Generic;
using Application.Interfaces;
using Domain.Entities.ITTools.LicenceAutoStacja;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public partial class Discounts2AS : DbContext, IDiscounts2AS
{
    public Discounts2AS()
    {
    }

    public Discounts2AS(DbContextOptions<Discounts2AS> options)
        : base(options)
    {
    }

    public virtual DbSet<SalonInfo> SalonInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalonInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SalonInfo");

            entity.Property(e => e.ArvalApi)
                .HasMaxLength(50)
                .HasColumnName("ARVAL_API");
            entity.Property(e => e.BlacharniaKierownik)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.BodyshopDealerId).HasColumnName("BodyshopDealerID");
            entity.Property(e => e.Cc).HasColumnName("CC");
            entity.Property(e => e.DatabaseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DocSuffix)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.LicencePrefix)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.ListaManage).IsUnicode(false);
            entity.Property(e => e.LogicalFileNameData)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LogicalFileNameLog)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MagazynDemo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MagazynKierownik)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.MagazynNowe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MagazynUsed)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MainDealerId).HasColumnName("MainDealerID");
            entity.Property(e => e.MarkaLkw).HasColumnName("MarkaLKW");
            entity.Property(e => e.MarkaVw).HasColumnName("MarkaVW");
            entity.Property(e => e.Marki)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MysystemDealer).HasColumnName("mysystem_dealer");
            entity.Property(e => e.Rejon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReportSourceName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SalonKierownik)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.SerwisKierownik)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
