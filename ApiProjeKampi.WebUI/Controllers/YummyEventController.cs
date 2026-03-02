using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.TestimonialDtos;
using ApiProjeKampi.WebUI.Dtos.YummyEventDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class YummyEventController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public YummyEventController(IHttpClientFactory httpClientFactory, ApiSettings apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }
        [AllowAnonymous]
        public async Task<IActionResult> YummyEventList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/YummyEvents");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultYummyEventDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateYummyEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateYummyEvent(CreateYummyEventDto createYummyEventDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createYummyEventDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(_apiSettings.BaseUrl +"/api/YummyEvents", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("YummyEventList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteYummyEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync(_apiSettings.BaseUrl +"/api/YummyEvents?id=" + id);
            return RedirectToAction("YummyEventList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateYummyEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/YummyEvents/GetYummyEvent?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetYummyEventByIdDto>(jsonData);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateYummyEvent(UpdateYummyEventDto updateYummyEventDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateYummyEventDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync(_apiSettings.BaseUrl +"/api/YummyEvents/", stringContent);
            return RedirectToAction("YummyEventList");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewYummyEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl +"/api/YummyEvents/GetYummyEvent?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetYummyEventByIdDto>(jsonData);
            return View(value);
        }
    }
}
