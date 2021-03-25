using CustomerApp.Core.Entities;
using CustomerApp.Infrastructure.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.DataAccess
{
    public class LoyaltyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public LoyaltyDbContext(DbContextOptions<LoyaltyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
        }
    }
}
