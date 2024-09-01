using MauiApp1.Scripts.Processors;

namespace MauiApp1.Scripts;

internal static class PagesTasker
{
    public static IEnumerable<Task> GetTasksFromPages(IEnumerable<Page> pages)
    {
        var tasks = new List<Task>();
        foreach (var page in pages)
        {
            var dataProcessor = page as IProcessDataInBackground;
            if (dataProcessor != null)
                tasks.Add(dataProcessor.GetProcessedTask());
        }

        return tasks;
    }
}
