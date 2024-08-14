using MauiApp1.Data.Storing;
using System.Text;
using System.Text.Json;

namespace MauiApp1.Data.Sending;

internal class DataSender : IDataSender
{
    private const string API_ENDPOINT = @"http://maluch3.mikr.us:20162/upload";
    private readonly HttpClient _httpClient;

    public DataSender()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string?> SendDataAsync(ISendDataHoldable dataHolder)
    {
        try
        {
            string jsonData = JsonSerializer.Serialize(dataHolder);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(API_ENDPOINT, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch(HttpRequestException ex)
        {
            Console.WriteLine("Error while sending data to API", ex.Message);
            return null;
        }
    }
}
