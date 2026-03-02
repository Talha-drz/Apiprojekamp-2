using ApiProjeKampi.WebUI.Dtos.AboutDtos;
using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents
{
    public class _AboutDefaultComponentPartial :ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _AboutDefaultComponentPartial(IHttpClientFactory httpClientFactory, ApiSettings apiSettings = null)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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
    }
}
