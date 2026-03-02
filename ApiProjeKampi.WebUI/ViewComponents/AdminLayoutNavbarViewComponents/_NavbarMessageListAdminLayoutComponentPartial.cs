using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.AdminLayoutNavbarViewComponents
{
    public class _NavbarMessageListAdminLayoutComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _NavbarMessageListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory, ApiSettings apiSettings = null)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/Messages/MessageListByIsReadFalse");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
