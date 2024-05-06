using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductImagesList()
        {
            var values = await _productImageService.GetAllProductImagesAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(string id)
        {
            var value = await _productImageService.GetByIdProductImageAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createCategoryDto)
        {
            await _productImageService.CreateProductImageAsync(createCategoryDto);
            return Ok("Product Images Başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            await _productImageService.DeleteProductImageAsync(id);
            return Ok("Product Images başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImages(UpdateProductImageDto updateCategoryDto)
        {
            await _productImageService.UpdateProductImageAsync(updateCategoryDto);
            return Ok("Product Images başarıyla güncellendi");
        }
    }
}
