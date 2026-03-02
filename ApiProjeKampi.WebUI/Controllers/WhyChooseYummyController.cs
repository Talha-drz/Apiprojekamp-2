using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ServiceDtos;
using ApiProjeKampi.WebUI.Dtos.WhyChooseYummyDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WhyChooseYummyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public WhyChooseYummyController(IHttpClientFactory httpClientFactory, ApiSettings apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }
        [AllowAnonymous]
        public async Task<IActionResult> WhyChooseYummyList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/Services");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhyChooseYummyDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateWhyChooseYummy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhyChooseYummy(CreateWhyChooseYummyDto createWhyChooseYummyDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createWhyChooseYummyDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(_apiSettings.BaseUrl +"/api/Services", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("WhyChooseYummyList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync(_apiSettings.BaseUrl +"/api/Services?id=" + id);
            return RedirectToAction("WhyChooseYummyList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/Services/GetService?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetWhyChooseYummyByIdDto>(jsonData);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWhyChooseYummy(UpdateWhyChooseYummyDto updateWhyChooseYummyDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateWhyChooseYummyDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync(_apiSettings.BaseUrl +"/api/Services/", stringContent);
            return RedirectToAction("WhyChooseYummyList");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/Services/GetService?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetWhyChooseYummyByIdDto>(jsonData);
            return View(value);
        }
    }
}
