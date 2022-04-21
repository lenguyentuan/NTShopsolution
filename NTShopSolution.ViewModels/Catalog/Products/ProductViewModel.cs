using System;

namespace NTShopSolution.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { set; get; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; } 
    }
}