using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürünlerin Listesi";
            ViewBag.V0 = "Ürünlerin Islemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7080/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("CreateProduct")]
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürünlerin Listesi";
            ViewBag.V0 = "Ürünlerin Islemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7080/api/categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryValues = (from c in values
                                                   select new SelectListItem
                                                   {
                                                       Text = c.CategoryName,
                                                       Value = c.CategoryID
                                                   }).ToList();
            ViewBag.CategoryValues=categoryValues;
            return View();
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7080/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7080/api/Products?id=" + Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateProduct/{Id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string Id)
        {
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Ürünler";
            ViewBag.V3 = "Ürünler Guncelleme Sayfası";
            ViewBag.V0 = "Ürünler Islemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7080/api/categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryValues1 = (from c in values
                                                   select new SelectListItem
                                                   {
                                                       Text = c.CategoryName,
                                                       Value = c.CategoryID
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues1;

            responseMessage = null;
            responseMessage = await client.GetAsync("https://localhost:7080/api/Products/" + Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonDate = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateProductDto>(jsonDate);
                return View(value);
            }
            return View();
        }

        [Route("UpdateProduct/{Id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7080/api/Products/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View();
        }
    }
}
