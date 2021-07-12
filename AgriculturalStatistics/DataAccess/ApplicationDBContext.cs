using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriculturalStatistics.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriculturalStatistics.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Commodity> Commodities { get; set; }
      
        public DbSet<Group> Groups { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Commodity>()
             .Property(fn => fn.CommodityID)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>()
            .Property(fn => fn.GroupID)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Sector>()
            .Property(fn => fn.SectorID)
            .ValueGeneratedOnAdd();

        }
    }
}
