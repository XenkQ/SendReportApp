namespace MauiApp1.Data.Waiting;

internal class DataWaiter<T> : IWaitForData<T>
{
    private T _dataToUpdateAfterNotification;

    public DataWaiter(T dataToUpdateAfterNotification)
    {
        _dataToUpdateAfterNotification = dataToUpdateAfterNotification;
    }

    public void OnNotification(T processedData)
        => UpdateDataAfterWait(_dataToUpdateAfterNotification, processedData);

    private void UpdateDataAfterWait(T data, T processedData)
        => _dataToUpdateAfterNotification = processedData;
}
