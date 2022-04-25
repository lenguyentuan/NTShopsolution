using NTShopSolution.ViewModels.Catalog.Products;
using NTShopSolution.ViewModels.Common;
using System.Threading.Tasks;

namespace NTShopSolution.Application.Catalog.Products.Dtos
{
    public interface IPublicProductService
    {
        public Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);
    }
}