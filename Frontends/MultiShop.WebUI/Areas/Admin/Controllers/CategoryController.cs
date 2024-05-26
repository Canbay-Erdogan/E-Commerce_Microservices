using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Kategoriler";
            ViewBag.V3 = "Kategori Listesi";
            ViewBag.V0 = "Kategori Islemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7080/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Kategoriler";
            ViewBag.V3 = "Yeni Kategori Girisi";
            ViewBag.V0 = "Kategori Islemleri";

            return View();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7080/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteCategory/{Id}")]
        public async Task<IActionResult> DeleteCategory (string Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7080/api/Categories?id=" + Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateCategory/{Id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string Id)
        {
            ViewBag.V1 = "Ana Sayfa";
            ViewBag.V2 = "Kategoriler";
            ViewBag.V3 = "Kategori Guncelleme Sayfası";
            ViewBag.V0 = "Kategori Islemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7080/api/Categories/" + Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonDate = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonDate);
                return View(value);
            }
            return View();
        }

        [Route("UpdateCategory/{Id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7080/api/Categories/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }
    }
}
