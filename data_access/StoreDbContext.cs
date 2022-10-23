using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Phone>().HasOne(p => p.Color)
                                        .WithMany(c => c.Phones)
                                        .HasForeignKey(p => p.ColorId);

            modelBuilder.SeedColors();
            modelBuilder.SeedPhones();
        }

        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
    }
}
