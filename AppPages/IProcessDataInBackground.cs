namespace MauiApp1.AppPages;

internal interface IProcessDataInBackground
{
    Task GetProcessedTask();
    void StartProcessingDataInBackground();
}