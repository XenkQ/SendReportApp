using MauiApp1.Data.Storing;
using MauiApp1.DTOs;

namespace MauiApp1.Data.Sending;

public interface IDataSender
{
    Task<HttpResponseMessage> SendDataAsync(ISendDataHoldable dataHolder, ApiSettings apiSettings);
}