using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        public ProductController(IHttpClientFactory httpClientFactory, ApiSettings apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }
        [AllowAnonymous]
        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/Products/ProductListWithCategory");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/Categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();

            ViewBag.v = categoryValues;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(_apiSettings.BaseUrl +"/api/Products/CreateProductWithCategory", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync(_apiSettings.BaseUrl +"/api/Products?id=" + id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/Products/GetProduct?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);

            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync(_apiSettings.BaseUrl +"/api/Categories");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData2);
            List<SelectListItem> categoryValues = (from x in values2
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();

            ViewBag.v = categoryValues;

            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync(_apiSettings.BaseUrl+"/api/Products/", stringContent);
            return RedirectToAction("ProductList");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // 1️⃣ Ürünü çek
            var responseMessage = await client.GetAsync(
                _apiSettings.BaseUrl+"/api/Products/GetProduct?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
                return NotFound();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);

            // 2️⃣ CategoryId varsa kategori adını çek
            if (value.CategoryId.HasValue)
            {
                var categoryResponse = await client.GetAsync(
                    _apiSettings.BaseUrl+"/api/Categories/GetCategory?id=" + value.CategoryId.Value);

                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                    var category = JsonConvert.DeserializeObject<GetCategoryByIdDto>(categoryJson);

                    ViewBag.CategoryName = category.categoryName;
                }
            }

            return View(value);
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductController controller &&
                   EqualityComparer<ApiSettings>.Default.Equals(_apiSettings, controller._apiSettings);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_apiSettings);
        }
    }
}
