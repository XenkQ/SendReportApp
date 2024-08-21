using MauiApp1.DTOs;
using Microsoft.Extensions.Options;

namespace MauiApp1.Connection;

public interface IServerConnectionChecker
{
    bool IsConnected();
}

internal class ApiConnection : IServerConnectionChecker
{
    private readonly ApiSettings _apiSettings;

    public ApiConnection(IOptions<ApiSettings> settings)
    {
        _apiSettings = settings.Value;
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
