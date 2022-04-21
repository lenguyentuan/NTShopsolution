using Microsoft.EntityFrameworkCore;
using NTShopSolution.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.OrderDate);
            builder.Property(x => x.ShipEmail).IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ShipAddress).IsRequired().HasMaxLength(12);

        }
    }
}
