using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NTShopSolution.Application.Common;
using NTShopSolution.Data.EF;
using NTShopSolution.Data.Models;
using NTShopSolution.Utilities.Exceptions;
using NTShopSolution.ViewModels.Catalog.Products;
using NTShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NTShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly NTShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(NTShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public Task<int> AddImages(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Set<Product>().FindAsync(productId);
            product.ViewCount++;
            await _context.SaveChangesAsync(); 
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            //Save image
            if(request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Set<Product>().Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Set<Product>().FindAsync(productId);
            if (product == null) throw new NTShopExceptions($"Cannot find a product: {productId}");

            var images = _context.Set<ProductImage>().Where(i => i.ProductId == productId);
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.Set<Product>().Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetPublicProductPagingRequest request)
        {
            var query = from p in _context.Set<Product>()
                        join pt in _context.Set<ProductTranslation>() on p.Id equals pt.ProductId
                        join pic in _context.Set<ProductInCategory>() on p.Id equals pic.ProductId
                        join c in _context.Set<Category>() on pic.CategoryId equals c.Id
                        select new { p,pt,pic};
            if (!string.IsNullOrEmpty(request.Keywork)) query = query.Where(x => x.pt.Name.Contains(request.Keywork));

            if(request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }

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

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var productImages = await _context.Set<ProductImage>().Where(p => p.ProductId == productId).ToListAsync();
            if (productImages == null) throw new NTShopExceptions($"can not find productImage with productId : {productId}");
            List<ProductImageViewModel> resultImages = new List<ProductImageViewModel>();
            foreach(var image in productImages)
            {
                resultImages.Add(new ProductImageViewModel() 
                {
                    Id = image.Id,
                    FilePath = image.ImagePath,
                    IsDefault = image.IsDefault,
                    FileSize = image.FileSize
                });
            }
            return resultImages;
        }

        public async Task<int> RemoveImages(int imageId)
        {
            try 
            {
                var productImage = await _context.Set<ProductImage>().FindAsync(imageId);
                _context.Set<ProductImage>().Remove(productImage);
                return await _context.SaveChangesAsync();
            }
            catch
            {
                throw new NTShopExceptions($"cannot find ProductImageId {imageId}");
            }
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Set<Product>().FindAsync(request.Id);
            var productTranslation = _context.Set<ProductTranslation>().FirstOrDefault(x => x.ProductId == request.Id);
            if (product == null|| productTranslation == null) throw new NTShopExceptions($"cannot find a product with id : {request.Id}");

            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Details = request.Details;
            productTranslation.Name = request.Name;
            productTranslation.Description = request.Description;

            //save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = _context.Set<ProductImage>().FirstOrDefault(i => i.IsDefault == true && i.ProductId == request.Id);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.Set<ProductImage>().Update(thumbnailImage);
                };
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImages(int imageId, string caption, bool isDefault)
        {
            var productImage = await _context.Set<ProductImage>().FindAsync(imageId);
            if (productImage == null) throw new NTShopExceptions($"Can not find ProductImage with Id: {imageId}");
            productImage.Caption = caption;
            if(productImage.IsDefault != isDefault)
            {
                productImage.IsDefault = isDefault;
            }
            _context.Set<ProductImage>().Update(productImage);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Set<Product>().FindAsync(productId);
            if (product == null) throw new NTShopExceptions($"cannot find a product with id : {productId}");
            product.Price = newPrice;

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Set<Product>().FindAsync(productId);
            if (product == null) throw new NTShopExceptions($"cannot find a product with id : {productId}");
            product.Stock = addedQuantity;

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }



    }
}
