using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Infrastruture.EFCore.EntityTypeConfigurations
{
    public class ProductEntityTypeConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            // One to many relationship with color.
            builder.HasOne(x => x.Color).WithMany(x => x.Products);
        }
    }
}
