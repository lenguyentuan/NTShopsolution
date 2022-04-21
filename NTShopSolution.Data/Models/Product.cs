using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.Models
{
    public class Product
    {
        public int Id { set; get; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { set; get; }

        public int Stock { get; set; }

        public int ViewCount { get; set; }

        public DateTime DateCreated { set; get; }

        public string SeoAlias { set; get; }

        public IList<OrderDetail> OrderDetails { set; get; }

        public IList<ProductInCategory> ProductInCategories { set; get; }

        public IList<ProductTranslation> ProductTranslations { set; get; }

        public IList<ProductInCart> ProductInCarts { set; get; }
        public IList<ProductImage> ProductImages { set; get; }
    }
}
