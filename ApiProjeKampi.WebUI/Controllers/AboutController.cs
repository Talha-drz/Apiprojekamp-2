using ApiProjeKampi.WebUI.Dtos.AboutDtos;
using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AboutController(IHttpClientFactory httpClientFactory, ApiSettings apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }
        [AllowAnonymous]
        public async Task<IActionResult> AboutList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Abouts");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(_apiSettings.BaseUrl+"/api/Abouts", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }

            return View();
        }
      
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync(_apiSettings.BaseUrl+"/api/Abouts?id=" + id);
            return RedirectToAction("AboutList");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Abouts/GetAbout?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetAboutByIdDto>(jsonData);
            return View(value);
        }
    
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync(_apiSettings.BaseUrl+"/api/Abouts/", stringContent);
            return RedirectToAction("AboutList");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Abouts/GetAbout?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetAboutByIdDto>(jsonData);
            return View(value);
        }
    }
}
