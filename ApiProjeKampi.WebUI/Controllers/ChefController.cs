using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChefController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public ChefController(IHttpClientFactory httpClientFactory, ApiSettings apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }
        [AllowAnonymous]
        public async Task<IActionResult> ChefList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Chefs");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateChef()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto createChefDtoDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createChefDtoDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(_apiSettings.BaseUrl+"/api/Chefs", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ChefList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync(_apiSettings.BaseUrl+"/api/Chefs?id=" + id);
            return RedirectToAction("ChefList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Chefs/GetChef?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetChefByIdDto>(jsonData);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefDtoDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateChefDtoDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync(_apiSettings.BaseUrl+"/api/Chefs/", stringContent);
            return RedirectToAction("ChefList");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Chefs/GetChef?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetChefByIdDto>(jsonData);
            return View(value);
        }
    }
}
