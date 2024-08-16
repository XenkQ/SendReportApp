using MauiApp1.Data.Waiting;

namespace MauiApp1.AppPages;

internal interface IProcessDataInBackground
{
    void NotifyAfterDataProcesing<T>(T processedData, IWaitForData<T> dataWaiter);
}