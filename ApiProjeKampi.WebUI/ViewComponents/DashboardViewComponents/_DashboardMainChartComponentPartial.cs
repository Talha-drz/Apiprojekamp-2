using ApiProjeKampi.WebUI.Dtos.ApiSettings;
using ApiProjeKampi.WebUI.Dtos.ReservationDtos;
using ApiProjeKampi.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardMainChartComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _settings;

        public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory, ApiSettings settings = null)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_settings.BaseUrl+"/");

            var response = await client.GetAsync("api/Reservations/GetReservationStats");
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<ReservationChartDto>>(json);

            return View(data);
        }
    }
}
