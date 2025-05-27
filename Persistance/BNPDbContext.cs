using System;
using System.Collections.Generic;
using Application.Interfaces;
using Domain.Entities.BNP;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public partial class BNPDbContext : DbContext, IBNPDbContext
{
    public BNPDbContext()
    {
    }

    public BNPDbContext(DbContextOptions<BNPDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bnp20> Bnp20s { get; set; }
    public virtual DbSet<Bnp55> Bnp55s { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bnp20>(entity =>
        {
            entity.HasKey(e => e.Txid);

            entity.ToTable("BNP20");

            entity.HasIndex(e => e.Data, "IDX_Data_BNP20");

            entity.Property(e => e.Txid)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("txid");
            entity.Property(e => e.Adres)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("adres");
            entity.Property(e => e.Cdtdbtind)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cdtdbtind");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Iban)
                .HasMaxLength(28)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iban");
            entity.Property(e => e.Instrid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("instrid");
            entity.Property(e => e.Kwota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kwota");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nazwa");
            entity.Property(e => e.Panstwo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("panstwo");
            entity.Property(e => e.Tytul)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tytul");
            entity.Property(e => e.Waluta)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("waluta");
        });

        modelBuilder.Entity<Bnp55>(entity =>
        {
            entity.HasKey(e => e.Txid);

            entity.ToTable("BNP55");

            entity.HasIndex(e => e.Data, "IDX_Data_BNP55");

            entity.Property(e => e.Txid)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("txid");
            entity.Property(e => e.Adres)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("adres");
            entity.Property(e => e.Cdtdbtind)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cdtdbtind");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Iban)
                .HasMaxLength(28)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iban");
            entity.Property(e => e.Instrid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("instrid");
            entity.Property(e => e.Kwota)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kwota");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nazwa");
            entity.Property(e => e.Panstwo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("panstwo");
            entity.Property(e => e.Tytul)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("tytul");
            entity.Property(e => e.Waluta)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("waluta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
