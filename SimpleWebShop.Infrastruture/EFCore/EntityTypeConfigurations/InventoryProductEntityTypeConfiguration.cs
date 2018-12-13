using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Infrastruture.EFCore.EntityTypeConfigurations
{
    public class InventoryProductEntityTypeConfiguration
        : IEntityTypeConfiguration<InventoryProduct>
    {
        public void Configure(EntityTypeBuilder<InventoryProduct> builder)
        {
            builder.HasKey(x => x.Id);

            // One to one relationship between Product and inventory.
            builder.HasOne(x => x.Product).WithOne(x => x.Inventory);

            // Standard information.
            builder.HasData(new InventoryProduct() { Id = 1, ProductId = 1, Amount = 20, Price = 20.0 });
        }
    }
}
