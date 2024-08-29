using MauiApp1.Model;

namespace MauiApp1.Scripts.Data.Sending;

public interface IDataSender
{
    Task<HttpResponseMessage> SendDataAsync(AlertDataToSend dataHolder);
}