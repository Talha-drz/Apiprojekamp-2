using Microsoft.AspNetCore.SignalR;

namespace ApiProjeKampi.WebUI.Models
{
    public class ChatHub : Hub
    {
        private const string apiKey = "";
        private const string model = "gpt-4o-mini";
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static readonly Dictionary<string, List<Dictionary<string, string>>> _history = new Dictionary<string, List<Dictionary<string, string>>>();

        public override Task OnConnectedAsync()
        {
            _history[Context.ConnectionId] = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                       ["role"] = "system", ["content"]="You are a helpful asisten. Keep answes concise."
                }
            };
              
        return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _history.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string userMessage)
        {

        }
    }
}
