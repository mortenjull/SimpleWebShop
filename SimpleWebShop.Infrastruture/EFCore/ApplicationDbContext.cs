using Microsoft.EntityFrameworkCore;
using SimpleWebShop.Infrastruture.EFCore.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Infrastruture.EFCore
{
    public class ApplicationDbContext
        : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ColorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryProductEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
