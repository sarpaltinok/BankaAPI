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
        public DbSet<OdemeLog> OdemeLoglari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
