namespace MauiApp1.Scripts.Data.Processors;

internal interface IProcessDataInBackground
{
    CancellationTokenSource CancellationTokenSource { get; }
    Task GetProcessedTask();
    Task StartProcessingDataInBackground();
}