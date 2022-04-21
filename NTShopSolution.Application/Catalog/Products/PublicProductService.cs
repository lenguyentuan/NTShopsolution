using Microsoft.EntityFrameworkCore;
using NTShopSolution.Application.Catalog.Products.Dtos;
using NTShopSolution.Data.EF;
using NTShopSolution.Data.Models;
using NTShopSolution.ViewModels.Catalog.Products;
using NTShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly NTShopDbContext _context;

        public PublicProductService(NTShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            // 1. Select join
            var query = from p in _context.products
                        join pt in _context.productTranslations on p.Id equals pt.ProductId
                        join pic in _context.productInCategories on p.Id equals pic.ProductId
                        join c in _context.categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            var productId = _context.products.Find(1).DateCreated;


            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.pt.Name,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount,

            }).ToListAsync();

            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetManageProductPagingRequest request)
        {

            // 1. Select join
            var query = from p in _context.Set<Product>()
                        join pt in _context.Set<ProductTranslation>() on p.Id equals pt.ProductId
                        join pic in _context.Set<ProductInCategory>() on p.Id equals pic.ProductId
                        join c in _context.Set<Category>() on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            // 2. filter

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }
            //3. paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,

                }).ToListAsync();

            //4 Select and projection
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;
        }
    }
}
