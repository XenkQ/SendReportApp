namespace MauiApp1.Data.Processors;

internal interface IProcessDataInBackground
{
    Task GetProcessedTask();
    void StartProcessingDataInBackground();
}