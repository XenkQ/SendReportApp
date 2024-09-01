using MauiApp1.Scripts.Processors;

namespace MauiApp1.Services;

public interface IFormBackgroundTaskObserver
{
    void Subscribe(IProcessDataInBackground subscriber);
    void Unsubscribe(IProcessDataInBackground subscriber);
    IEnumerable<Task> GetAllTasksFromObservedDataProcessors();
}

public sealed class FormBackgroundTaskObserver : IFormBackgroundTaskObserver
{
    private readonly HashSet<IProcessDataInBackground> _formsProcessingDataInBackground = new();

    public void Subscribe(IProcessDataInBackground subscriber)
        => _formsProcessingDataInBackground.Add(subscriber);

    public void Unsubscribe(IProcessDataInBackground subscriber)
        => _formsProcessingDataInBackground.Remove(subscriber);

    public IEnumerable<Task> GetAllTasksFromObservedDataProcessors()
        => _formsProcessingDataInBackground.Select(dataProcessor => dataProcessor.GetProcessedTask());
}
