namespace MauiApp1.Scripts.Connection;

public interface IServerConnectionChecker
{
    bool IsConnected();
}

internal class ApiConnection : IServerConnectionChecker
{
    private readonly ApiSettings _apiSettings;

    public ApiConnection(ApiSettings apiSettings)
    {
        _apiSettings = apiSettings;
    }

    public bool IsConnected()
    {
        try
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var response = client.GetAsync(_apiSettings.StatusPath).Result;
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
