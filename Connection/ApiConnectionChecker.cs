public interface IServerConnectionChecker
{
    Task<bool> IsConnectedAsync(string apiEndpoint);
}

namespace MauiApp1.Connection
{
    internal class ApiConnectionChecker : IServerConnectionChecker
    {
        public async Task<bool> IsConnectedAsync(string apiEndpoint)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiEndpoint);
            var response = await client.GetAsync("/status");
            return response.IsSuccessStatusCode;
        }
    }
}
