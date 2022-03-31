using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTShopSolution.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.Configuration
{
    public class ProductInCartConfiguration : IEntityTypeConfiguration<ProductInCart>
    {
        public void Configure(EntityTypeBuilder<ProductInCart> builder)
        {
            builder.ToTable("ProductInCarts");
            builder.HasKey(x => new { x.CartId, x.ProductId });

            builder.HasOne(x => x.Cart).WithMany(x => x.ProductInCarts).HasForeignKey(x => x.CartId);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductInCarts).HasForeignKey(x => x.ProductId);
        }
    }
}
