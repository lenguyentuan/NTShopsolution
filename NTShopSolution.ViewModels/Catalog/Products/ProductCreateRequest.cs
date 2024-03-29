﻿using Microsoft.AspNetCore.Http;

namespace NTShopSolution.ViewModels.Catalog.Products
{ 
    public class ProductCreateRequest
    {
        public decimal Price { get; set; }
        public decimal OriginalPrice { set; get; }
        public int Stock { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoAlias { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public IFormFile ThumbnailImage { set; get; }


    }
}
