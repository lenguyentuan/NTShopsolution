using Microsoft.AspNetCore.Http;

namespace NTShopSolution.ViewModels.Catalog.ProductImages
{
    public class ProductImageCreateRequest
    {
        public string Caption { set; get; }
        public bool IsDefault { set; get; }
        public int SortOrder { set; get; }
        public IFormFile ImageFile { set; get; }
    }
}