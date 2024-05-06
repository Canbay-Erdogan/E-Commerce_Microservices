using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;
        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productDetailService.GetAllProductDetailsAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetproductDetailById(string id)
        {
            var value = await _productDetailService.GetByIdProductDetailAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateproductDetail(CreateProductDetailDto createproductDetailDto)
        {
            await _productDetailService.CreateProductDetailAsync(createproductDetailDto);
            return Ok("Ürün detay Başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteproductDetail(string id)
        {
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("Ürün detay başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateproductDetail(UpdateProductDetailDto updateproductDetailDto)
        {
            await _productDetailService.UpdateProductDetailAsync(updateproductDetailDto);
            return Ok("Ürün detay başarıyla güncellendi");
        }
    }
}
