using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PersistenceLayer.Data
{
    namespace PersistenceLayer.Data
    {
        public class StoreDbContext : DbContext
        {
            public StoreDbContext(DbContextOptions<StoreDbContext> options)
                : base(options)
            {
            }

            public DbSet<Product> Products { get; set; }
            public DbSet<ProductBrand> ProductBrands { get; set; }
            public DbSet<ProductType> ProductTypes { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
            }
        }
    }
}