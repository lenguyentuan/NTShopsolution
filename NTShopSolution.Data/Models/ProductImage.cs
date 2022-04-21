using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.Data.Models
{
    public class ProductImage
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public string ImagePath { set; get; }
        public string Caption { set; get; }
        public bool IsDefault { set; get; }
        public DateTime DateCreated { get; set; }
        public int SortOrder { set; get; }
        public long FileSize { set; get; }
        public Product Product { get; set; }
    }
}
