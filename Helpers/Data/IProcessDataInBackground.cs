namespace MauiApp1.Scripts.Processors;

public interface IProcessDataInBackground
{
    CancellationTokenSource CancellationTokenSource { get; }
    Task GetProcessedTask();
    Task StartProcessingDataInBackground();
}