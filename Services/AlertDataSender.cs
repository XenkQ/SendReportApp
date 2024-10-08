﻿using MauiApp1.Model;
using MauiApp1.Model.Settings;
using System.Text;
using System.Text.Json;

namespace MauiApp1.Services;

public interface IAlertDataSender
{
    Task<HttpResponseMessage> SendDataAsync(AlertDataToSend dataHolder);
}

internal class AlertDataSender : IAlertDataSender
{
    private readonly ApiSettings _apiSettings;

    public AlertDataSender(ApiSettings apiSettings)
    {
        _apiSettings = apiSettings;
    }

    public async Task<HttpResponseMessage> SendDataAsync(AlertDataToSend alertDataToSend)
    {
        try
        {
            string jsonData = JsonSerializer.Serialize(alertDataToSend);
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_apiSettings.UploadPath, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request Error while sending data to API", ex.Message);
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while sending data to API", ex.Message);
            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
