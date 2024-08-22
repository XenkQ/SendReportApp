﻿using MauiApp1.Data.Storing;
using MauiApp1.DTOs;
using System.Text;
using System.Text.Json;

namespace MauiApp1.Data.Sending;

internal class ApiDataSender : IDataSender
{
    public async Task<HttpResponseMessage> SendDataAsync(ISendDataHoldable dataHolder, ApiSettings apiSettings)
    {
        try
        {
            string jsonData = JsonSerializer.Serialize(dataHolder);
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiSettings.BaseUrl);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(apiSettings.UploadPath, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
        catch(HttpRequestException ex)
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
