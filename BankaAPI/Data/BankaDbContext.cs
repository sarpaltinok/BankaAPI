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
            base.OnModelCreating(modelBuilder);

            //Odemeler -> Musteriler (Many-to-One)
            modelBuilder.Entity<Odemeler>()
                .HasOne(o => o.Musteri)
                .WithMany(m => m.Odemeler)
                .HasForeignKey(o => o.MusteriNo)
                .OnDelete(DeleteBehavior.SetNull);

            //OdemeLog -> Musteriler (Many-to-One)
            modelBuilder.Entity<OdemeLog>()
                .HasOne(l => l.Musteri)
                .WithMany(m => m.OdemeLog)
                .HasForeignKey(l => l.MusteriNo)            
                .OnDelete(DeleteBehavior.SetNull);
        }
        
           
    }
}

