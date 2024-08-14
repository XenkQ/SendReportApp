using MauiApp1.Data.Storing;

namespace MauiApp1.Data.Sending;

public interface IDataSender
{
    Task<string> SendDataAsync(ISendDataHoldable dataHolder);
}