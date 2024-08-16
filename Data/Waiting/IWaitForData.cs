namespace MauiApp1.Data.Waiting;

public interface IWaitForData<T>
{
    void OnNotification(T processedData);
}