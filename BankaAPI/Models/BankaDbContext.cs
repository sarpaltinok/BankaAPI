using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankaAPI.Models;

public partial class BankaDbContext : DbContext
{
    public BankaDbContext()
    {
    }

    public BankaDbContext(DbContextOptions<BankaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Musteriler> Musteriler { get; set; }

    public virtual DbSet<OdemeLog> OdemeLogs { get; set; }

    public virtual DbSet<Odemeler> Odemelers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=BankaDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musteriler>(entity =>
        {
            entity.HasKey(e => e.MusteriNo).HasName("PK__Musteril__72627C22DC9A5328");

            entity.ToTable("Musteriler");

            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.Cinsiyet).HasMaxLength(10);
            entity.Property(e => e.KayitTarihi).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.KrediTutari).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Soyad).HasMaxLength(50);
            entity.Property(e => e.Sube).HasMaxLength(50);
            entity.Property(e => e.Telefon).HasMaxLength(15);
        });

        modelBuilder.Entity<OdemeLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__OdemeLog__5E5499A8071D15AD");

            entity.ToTable("OdemeLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Aciklama).HasMaxLength(100);
            entity.Property(e => e.OdemeTarihi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OdemeTutari).HasColumnType("decimal(18, 2)");

            object value = entity.HasOne(d => d.MusteriNoNavigation).WithMany(p => p.OdemeLogs)
                .HasForeignKey(d => d.MusteriNo)
                .HasConstraintName("FK__OdemeLog__Muster__3D5E1FD2");
        });

        modelBuilder.Entity<Odemeler>(entity =>
        {
            entity.HasKey(e => e.OdemeId).HasName("PK__Odemeler__B11B66ADBC78ECEB");

            entity.ToTable("Odemeler");

            entity.Property(e => e.OdemeId).HasColumnName("OdemeID");
            entity.Property(e => e.GecikmisBorcTutari).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GuncelBorcTutari).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GuncelOdemeTutari).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OdenmisBorcTutari).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MusteriNoNavigation).WithMany(p => p.Odemelers)
                .HasForeignKey(d => d.MusteriNo)
                .HasConstraintName("FK__Odemeler__Muster__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
