using NTShopSolution.ViewModels.Common;

namespace NTShopSolution.ViewModels.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingResultBase
    { 
        public int? CategoryId { set; get; }
    }
}
