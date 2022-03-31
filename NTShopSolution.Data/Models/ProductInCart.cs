using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.Models
{
    public class ProductInCart
    {
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public int ProductId { set; get; }

        public Product Product { set; get; }
    }
}
