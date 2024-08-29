using MauiApp1.Model;

namespace MauiApp1.Data.Sending;

public interface IDataSender
{
    Task<HttpResponseMessage> SendDataAsync(AlertDataToSend dataHolder);
}