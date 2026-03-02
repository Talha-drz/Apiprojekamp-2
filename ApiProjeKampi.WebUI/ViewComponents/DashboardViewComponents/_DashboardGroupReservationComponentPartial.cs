using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.GrupReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardGroupReservationComponentPartial :ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _DashboardGroupReservationComponentPartial(IHttpClientFactory httpClientFactory, ApiSettings apiSettings = null)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(_apiSettings.BaseUrl+"/api/GroupReservations/");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGroupReservationDto>>(jsonData);
                return View(values);
            }
            return View();

        }
    }
}
