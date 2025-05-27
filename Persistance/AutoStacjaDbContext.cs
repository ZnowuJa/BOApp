using System;
using System.Collections.Generic;
using Application.Interfaces;
using Domain.Entities.ITTools.LicenceAutoStacja;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public partial class AutoStacjaDbContext : DbContext, IAutoStacjaDbContext
{
    public AutoStacjaDbContext()
    {
    }

    public AutoStacjaDbContext(DbContextOptions<AutoStacjaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MysystemPunkt> MysystemPunkts { get; set; }
    public virtual DbSet<MysystemPunktCon> MysystemPunktCons { get; set; }
    
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MysystemPunkt>(entity =>
        {
            entity.ToTable("MYSYSTEM_PUNKT");

            entity.HasIndex(e => e.JednostkaOrgId, "IX_FK_MYSYSTEM_PUNKT_JEDNOSTKA_ORG").HasFillFactor(95);

            entity.HasIndex(e => e.Nazwa, "IX_MYSYSTEM_PUNKT_NAZWA");

            entity.Property(e => e.MysystemPunktId).HasColumnName("MYSYSTEM_PUNKT_ID");
            entity.Property(e => e.JednostkaOrgId).HasColumnName("JEDNOSTKA_ORG_ID");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAZWA");
            entity.Property(e => e.PunktEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Email kontaktowy do wydruków w zależności  z którego pkt zostanie wykonany wydruk")
                .HasColumnName("PUNKT_EMAIL");
            entity.Property(e => e.PunktTelefon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Numer kontaktowy do wydruków w zależności  z którego pkt zostanie wykonany wydruk")
                .HasColumnName("PUNKT_TELEFON");
        });
        
        modelBuilder.Entity<MysystemPunktCon>(entity =>
        {
            entity.ToTable("MYSYSTEM_PUNKT_CON", tb => tb.HasComment("Tabela definiująca zmiany kontekstu między punktami różnych jednostek"));

            entity.Property(e => e.MysystemPunktConId)
                .HasComment("Klucz główny")
                .HasColumnName("MYSYSTEM_PUNKT_CON_ID");
            entity.Property(e => e.MysystemPunktInId)
                .HasComment("Punkt logowania źródłowy")
                .HasColumnName("MYSYSTEM_PUNKT_IN_ID");
            entity.Property(e => e.MysystemPunktOutId)
                .HasComment("Punkt logowania docelowy")
                .HasColumnName("MYSYSTEM_PUNKT_OUT_ID");

            entity.HasOne(d => d.MysystemPunktIn).WithMany(p => p.MysystemPunktConMysystemPunktIns)
                .HasForeignKey(d => d.MysystemPunktInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MYSYSTEM_PUNKT_CON_MYSYSTEM_PUNKT");

            entity.HasOne(d => d.MysystemPunktOut).WithMany(p => p.MysystemPunktConMysystemPunktOuts)
                .HasForeignKey(d => d.MysystemPunktOutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MYSYSTEM_PUNKT_CON_MYSYSTEM_PUNKT1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
