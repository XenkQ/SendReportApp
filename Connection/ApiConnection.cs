public interface IServerConnectionChecker
{
    bool IsConnected(string apiEndpoint, string statusRoute);
}

namespace MauiApp1.Connection
{
    internal class ApiConnection : IServerConnectionChecker
    {
        public bool IsConnected(string apiEndpoint, string statusRoute)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiEndpoint);
                var response = client.GetAsync(statusRoute).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
