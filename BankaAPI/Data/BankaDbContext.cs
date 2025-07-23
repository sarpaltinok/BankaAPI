using BankaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BankaAPI.Data
{
    public class BankaDbContext : DbContext
    {
        public BankaDbContext(DbContextOptions<BankaDbContext> options) : base(options)
        {
        }

        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Odemeler> Odemeler { get; set; }
        public DbSet<OdemeLog> OdemeLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Musteri entity konfigürasyonu
            modelBuilder.Entity<Musteri>(entity =>
            {
                entity.HasKey(m => m.MusteriNo);

                // Decimal property'ler için precision
                entity.Property(m => m.KrediTutari)
                    .HasColumnType("decimal(18,2)");

                // Diğer konfigürasyonlar...
            });

            // Odemeler entity konfigürasyonu
            modelBuilder.Entity<Odemeler>(entity =>
            {
                entity.HasKey(o => o.OdemeId);

                // Foreign key ilişkisi
                entity.HasOne<Musteri>()
                    .WithMany(m => m.Odemelers)
                    .HasForeignKey(o => o.MusteriNo)
                    .OnDelete(DeleteBehavior.SetNull);

                // Decimal property'ler için precision
                entity.Property(o => o.GuncelOdemeTutari)
                    .HasColumnType("decimal(18,2)");
                entity.Property(o => o.GuncelBorcTutari)
                    .HasColumnType("decimal(18,2)");
                entity.Property(o => o.GecikmisBorcTutari)
                    .HasColumnType("decimal(18,2)");
                entity.Property(o => o.OdenmisBorcTutari)
                    .HasColumnType("decimal(18,2)");

                // DateOnly yerine DateTime kullanılıyorsa
                entity.Property(o => o.SonOdemeTarihi)
                    .HasColumnType("date");
            });

            // OdemeLog entity konfigürasyonu
            modelBuilder.Entity<OdemeLog>(entity =>
            {
                entity.HasKey(ol => ol.LogId);

                entity.Property(ol => ol.OdemeTutari)
                    .HasColumnType("decimal(18,2)");

                // Diğer konfigürasyonlar...
            });
        }
    }
}

