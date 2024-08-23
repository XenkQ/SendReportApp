namespace MauiApp1.Data.Processors;

internal interface IProcessDataInBackground
{
    CancellationTokenSource CancellationTokenSource { get; }
    Task GetProcessedTask();
    Task StartProcessingDataInBackground();
}