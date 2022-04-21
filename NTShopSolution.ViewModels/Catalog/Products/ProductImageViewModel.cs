using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.ViewModels.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string FilePath { set; get; }
        public bool IsDefault { set; get; }
        public long FileSize { set; get; }
    }
}
