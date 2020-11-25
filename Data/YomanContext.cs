using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using $safeprojectname$.Models;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$.Data
{
    public class YomanContext : DbContext
    {
        public YomanContext(DbContextOptions<YomanContext> options) : base(options)
        {
        }

        public DbSet<Meeting> Meeting { get; set; }
        public DbSet<MType> MType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<MType>().ToTable("MType");
        }
    }
}
