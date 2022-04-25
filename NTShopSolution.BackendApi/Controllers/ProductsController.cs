using Microsoft.AspNetCore.Mvc;
using NTShopSolution.Application.Catalog.Products;
using NTShopSolution.Application.Catalog.Products.Dtos;
using NTShopSolution.ViewModels.Catalog.ProductImages;
using NTShopSolution.ViewModels.Catalog.Products;
using System.Threading.Tasks;

namespace NTShopSolution.BackendApi.Controllers
{
    //http://localhost:port/api/product
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;

        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet("public-paging/{languagueId}")]
        public async Task<IActionResult> Get(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(languageId, request);
            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var products = await _manageProductService.GetById(productId, languageId);
            if (products == null)
                return BadRequest("Cannot find product");
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _manageProductService.GetById(productId, request.LanguageId);
            return Created(nameof(GetById), product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]   
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var result = await _manageProductService.UpdatePrice(productId, newPrice);
            if (!result)
            {
                BadRequest();
            }
            return Ok();
        }



        [HttpPatch("{productId}/addViewCount")]
        public async Task<IActionResult> AddViewCount(int productId)
        {
            var isSuccessful = await _manageProductService.AddViewCount(productId);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        // Images
        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest($"Cannot find product with image Id: {imageId}");
            return Ok(image);
        }
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();
            var product = await _manageProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _manageProductService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _manageProductService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }


    }
}