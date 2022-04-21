using NTShopSolution.ViewModels.Common;
using System.Collections.Generic;

namespace NTShopSolution.ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingResultBase
    {
        public string Keywork { get; set; }
        public List<int> CategoryIds { set; get; }
    }
}
