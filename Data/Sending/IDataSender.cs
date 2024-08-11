using MauiApp1.Data.Storing;

namespace MauiApp1.Data.Sending;

public interface IDataSender
{
    void SendData(ISendDataHoldable dataHolder);
}