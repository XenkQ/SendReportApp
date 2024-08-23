using MauiApp1.Data.Storing;
using MauiApp1.DTOs;

namespace MauiApp1.Data.Sending;

public interface IDataSender
{
    Task<HttpResponseMessage> SendDataAsync(IAlertDataToSend dataHolder);
}