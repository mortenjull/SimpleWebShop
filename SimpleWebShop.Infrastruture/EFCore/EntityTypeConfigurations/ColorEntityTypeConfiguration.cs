using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Infrastruture.EFCore.EntityTypeConfigurations
{
    public class ColorEntityTypeConfiguration
        : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
